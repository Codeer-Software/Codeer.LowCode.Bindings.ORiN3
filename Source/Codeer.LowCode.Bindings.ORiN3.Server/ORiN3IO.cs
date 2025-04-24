using Codeer.LowCode.Bindings.ORiN3.Designs;
using Codeer.LowCode.Blazor.DesignLogic;
using Codeer.LowCode.Blazor.Repository;
using Colda.CommonUtilities.Tasks;
using Design.ORiN3.Provider.V1.Base;
using System.Text.Json.Nodes;

namespace Codeer.LowCode.Bindings.ORiN3.Server
{
    public class ORiN3IO
    {
        private ORiN3FieldDesign? _design;
        private IList<ORiN3Provider> _providers = [];
        private AsyncLock _lock = new();
        private string _designFileDirector;
        private O3Setting _o3Setting;
        private O3TreeSetting _o3TreeSetting;
        private IDictionary<string, MultiTypeValue> _variableBuffer;

        public ORiN3IO(string designFileDirectory)
            => _designFileDirector = designFileDirectory;

        private static async Task<ORiN3Provider> WakeupOrAttachProviderAsync(O3Setting.ORiN3RemoteEngineSetting remoteEngineSetting, O3Setting.ORiN3RootObjectSetting rootSetting, CancellationToken token)
        {
            using var remoteEngine = await ORiN3RemoteEngine.AttachAsync(remoteEngineSetting, token).ConfigureAwait(false);
            return await remoteEngine.WakeupOrAttachProviderAsync(rootSetting, token).ConfigureAwait(false);
        }

        public async Task SetDesignAsync(ORiN3FieldDesign? design)
        {
            if (design == null || ReferenceEquals(_design, design))
            {
                return;
            }
            _design = design;

            using var cts = new CancellationTokenSource();
            await InitAsync(cts.Token).ConfigureAwait(false);
        }

        private async Task InitAsync(CancellationToken token)
        {
            using (await _lock.LockAsync(token).ConfigureAwait(false))
            {
                ReadO3Json();
                ReadO3TreeJson();

                CreateVariableBuffer();

                // Launching Provider
                var remoteEngineId = _o3TreeSetting.Objects[0].Id; // Remote Engineは1つしか設定できないので要素0決め打ち
                var remoteEngineSetting = _o3Setting.GetRemoteEngine(remoteEngineId);

                foreach (var it in _o3TreeSetting.Objects[0].Children)
                {
                    var rootSetting = _o3Setting.GetRootObject(it.Id);
                    _providers.Add(await WakeupOrAttachProviderAsync(remoteEngineSetting, rootSetting, token).ConfigureAwait(false));
                }

                var parents = new Dictionary<string, IORiN3Object>();
                foreach (var provider in _providers)
                {
                    await provider.CreateObjectAsync(_o3Setting, _o3TreeSetting, token).ConfigureAwait(false);
                }

                Task.Factory.StartNew(() => UpdateBufferTask(CancellationToken.None));
            }
        }

        private async Task UpdateBufferTask(CancellationToken token)
        {
            while (true)
            {
                await Task.Delay(_design!.PollingIntervalMSec).ConfigureAwait(false);
                using (await _lock.LockAsync(token).ConfigureAwait(false))
                {
                    foreach (var provider in _providers)
                    {
                        try
                        {
                            var values = await provider.GetValuesAsync(token).ConfigureAwait(false);
                            foreach (var value in values)
                            {
                                _variableBuffer[value.Key] = MultiTypeValue.Create(value.Value);
                            }
                        }
                        catch (Exception ex)
                        {
                            // Handle exception
                        }
                    }
                }
            }
        }

        private void CreateVariableBuffer()
        {
            _variableBuffer = new Dictionary<string, MultiTypeValue>();
            foreach (var variable in _o3Setting.Variables)
            {
                _variableBuffer.Add(variable.Name, MultiTypeValue.Create(null));
            }
        }

        private void ReadO3Json()
        {
            using var o3File = DesignDataFileManager.GetResource(_designFileDirector, _design!.O3JsonFilePath);
            o3File!.Position = 0;
            var jsonNode = JsonNode.Parse(o3File);
            _o3Setting = new(jsonNode!);
        }

        private void ReadO3TreeJson()
        {
            using var o3TreeFile = DesignDataFileManager.GetResource(_designFileDirector, _design!.O3TreeJsonFilePath);
            o3TreeFile!.Position = 0;
            var jsonNode = JsonNode.Parse(o3TreeFile);
            _o3TreeSetting = new(jsonNode!);
        }

        public async Task<Dictionary<string, MultiTypeValue>> GetValuesAsync(List<string> devices)
        {
            using (await _lock.LockAsync())
            {
                var dic = new Dictionary<string, MultiTypeValue>();
                foreach (var device in devices)
                {
                    if (_variableBuffer.ContainsKey(device))
                    {
                        dic[device] = _variableBuffer[device];
                    }
                    else
                    {
                        dic[device] = MultiTypeValue.Create(null);
                    }
                }
                await Task.CompletedTask;
                return dic;
            }
        }
    }
}
