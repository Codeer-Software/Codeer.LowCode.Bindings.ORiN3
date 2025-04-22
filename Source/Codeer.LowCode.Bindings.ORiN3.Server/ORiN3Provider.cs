using Codeer.LowCode.Bindings.ORiN3.Server.TypeBranch;
using Codeer.LowCode.Blazor.Repository;
using Design.ORiN3.Provider.V1;
using Design.ORiN3.Provider.V1.Base;
using Design.ORiN3.Provider.V1.Characteristic;
using Message.ORiN3.Provider.V1.Branch.Switcher;
using System.Diagnostics;
using System.Text.Json;

namespace Codeer.LowCode.Bindings.ORiN3.Server
{
    internal class ORiN3Provider : IDisposable
    {
        private class ORiN3Root(IRootObject rootObject) : ORiN3Object(rootObject, string.Empty)
        {
            public IRootObject RootObject { get; } = rootObject;
        }

        private class ORiN3Object(IORiN3Object orin3Object, string key)
        {
            public string Key { get; } = key;
            public IORiN3Object Self { get; } = orin3Object;
            public IList<ORiN3Object> Children { get; } = [];
        }

        private IRootObject? _rootObject;
        private readonly bool _shutdonwWhenDisposing;
        private bool _disposedValue;
        private readonly ORiN3Root _objectTree;

        public ORiN3Provider(IRootObject rootObject, bool shutdonwWhenDisposing)
        {
            _rootObject = rootObject;
            _objectTree = new ORiN3Root(rootObject);
            _shutdonwWhenDisposing = shutdonwWhenDisposing;
        }

        ~ORiN3Provider()
        {
            Debug.Assert(false);
            Dispose(disposing: false);
        }

        internal async Task<MultiTypeValue> GetValueAsync(string key, CancellationToken token)
        {
            var orin3Object = GetObjectByKey(key);
            var variable = (IVariable)orin3Object.Self;
            var branch = new CreateMultiTypeValueBranch(variable);
            await TypeSwitcher.ExecuteAsync(variable.ORiN3ValueType, branch, token);
            return branch.Value;
        }

        private IEnumerable<ORiN3Object> EnumObject()
        {
            foreach (var it in _objectTree.Children)
            {
                foreach (var it2 in EnumObject(it))
                {
                    yield return it2;
                }
                yield return it;
            }
        }

        private IEnumerable<ORiN3Object> EnumObject(ORiN3Object orin3Object)
        {
            foreach (var it in orin3Object.Children)
            {
                foreach (var it2 in EnumObject(it))
                {
                    yield return it2;
                }
                yield return it;
            }
        }

        private ORiN3Object GetObjectByKey(string? key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return _objectTree;
            }

            foreach (var it in EnumObject())
            {
                if (it.Key == key)
                {
                    return it;
                }
            }

            throw new Exception();
        }

        private ORiN3Object GetObjectByName(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return _objectTree;
            }

            foreach (var it in EnumObject())
            {
                if (it.Self.Name == name)
                {
                    return it;
                }
            }

            throw new Exception();
        }

        internal async Task CreateObjectAsync(string setting, CancellationToken token)
        {
            using var json = JsonDocument.Parse(setting);
            var rootElement = json.RootElement;
            var parent = rootElement.GetProperty("parent").GetString();
            var key = rootElement.GetProperty("key").GetString()!;
            var name = rootElement.GetProperty("name").GetString()!;
            var typeName = rootElement.GetProperty("typeName").GetString()!;
            var option = rootElement.GetProperty("option").GetString()!;
            var objectType = rootElement.GetProperty("objectType").GetString();
            var variableType = rootElement.TryGetProperty("variableType", out var prop) ? prop.GetString() : null;

            if (objectType == "Controller")
            {
                var orin3Object = GetObjectByName(parent);
                var controller = await ((IControllerCreator)orin3Object.Self).CreateControllerAsync(
                    name: name,
                    typeName: typeName,
                    option: option,
                    token: token);
                orin3Object.Children.Add(new ORiN3Object(controller, key));
            }
            else if (objectType == "Variable")
            {
                var orin3Object = GetObjectByName(parent);
                if (variableType == "bool")
                {
                    var variable = await ((IChildCreator)orin3Object.Self).CreateVariableAsync<bool>(
                        name: name,
                        typeName: typeName,
                        option: option,
                        token: token);
                    orin3Object.Children.Add(new ORiN3Object(variable, key));
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (_shutdonwWhenDisposing)
                    {
                        _rootObject!.ShutdownAsync().Wait();
                    }
                    _rootObject!.Dispose();
                    _rootObject = null;
                }
                _disposedValue = true;
            }
        }
    }
}
