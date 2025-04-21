using Codeer.LowCode.Bindings.ORiN3.Designs;
using Codeer.LowCode.Blazor.Repository;
using Design.ORiN3.Common.V1;
using Design.ORiN3.Provider.Core.V1.Telemetry;
using Design.ORiN3.Provider.V1;
using Design.ORiN3.Provider.V1.Base;
using Design.ORiN3.Provider.V1.Characteristic;
using Grpc.Net.Client;
using Message.Client.ORiN3.Provider.V1;
using Message.Client.ORiN3.RemoteEngine.V1;
using Message.ORiN3.Provider.V1.Branch.Switcher;
using System.Text.Json;

namespace Codeer.LowCode.Bindings.ORiN3.Server
{
    public class ORiN3IO
    {
        internal class CreateMultiTypeValueBranch : IValueTypeBranchAsync
        {
            private readonly IVariable _variable;

            public MultiTypeValue Value { get; private set; } = MultiTypeValue.Create(null);

            public CreateMultiTypeValueBranch(IVariable variable)
            {
                _variable = variable;
            }

            public async Task CaseOfBoolAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<bool>)_variable).GetValueAsync(token));
            public async Task CaseOfBoolArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<bool[]>)_variable).GetValueAsync(token));
            public async Task CaseOfNullableBoolAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<bool?>)_variable).GetValueAsync(token));
            public async Task CaseOfNullableBoolArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<bool?[]>)_variable).GetValueAsync(token));
            public async Task CaseOfInt8Async(CancellationToken token) => Value = MultiTypeValue.Create(await((IVariable<sbyte>)_variable).GetValueAsync(token));
            public async Task CaseOfInt8ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<sbyte[]>)_variable).GetValueAsync(token));
            public async Task CaseOfNullableInt8Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<sbyte?>)_variable).GetValueAsync(token));
            public async Task CaseOfNullableInt8ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<sbyte?[]>)_variable).GetValueAsync(token));
            public async Task CaseOfInt16Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<short>)_variable).GetValueAsync(token));
            public async Task CaseOfInt16ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<short[]>)_variable).GetValueAsync(token));
            public async Task CaseOfNullableInt16Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<short?>)_variable).GetValueAsync(token));
            public async Task CaseOfNullableInt16ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<short?[]>)_variable).GetValueAsync(token));
            public async Task CaseOfInt32Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<int>)_variable).GetValueAsync(token));
            public async Task CaseOfInt32ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<int[]>)_variable).GetValueAsync(token));
            public async Task CaseOfNullableInt32Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<int?>)_variable).GetValueAsync(token));
            public async Task CaseOfNullableInt32ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<int?[]>)_variable).GetValueAsync(token));
            public async Task CaseOfInt64Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<long>)_variable).GetValueAsync(token));
            public async Task CaseOfInt64ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<long[]>)_variable).GetValueAsync(token));
            public async Task CaseOfNullableInt64Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<long?>)_variable).GetValueAsync(token));
            public async Task CaseOfNullableInt64ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<long?[]>)_variable).GetValueAsync(token));
            public async Task CaseOfUInt8Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<byte>)_variable).GetValueAsync(token));
            public async Task CaseOfUInt8ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<byte[]>)_variable).GetValueAsync(token));
            public async Task CaseOfNullableUInt8Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<byte?>)_variable).GetValueAsync(token));
            public async Task CaseOfNullableUInt8ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<byte?[]>)_variable).GetValueAsync(token));
            public async Task CaseOfUInt16Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<ushort>)_variable).GetValueAsync(token));
            public async Task CaseOfUInt16ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<ushort[]>)_variable).GetValueAsync(token));
            public async Task CaseOfNullableUInt16Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<ushort?>)_variable).GetValueAsync(token));
            public async Task CaseOfNullableUInt16ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<ushort?[]>)_variable).GetValueAsync(token));
            public async Task CaseOfUInt32Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<uint>)_variable).GetValueAsync(token));
            public async Task CaseOfUInt32ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<uint[]>)_variable).GetValueAsync(token));
            public async Task CaseOfNullableUInt32Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<uint?>)_variable).GetValueAsync(token));
            public async Task CaseOfNullableUInt32ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable <uint?[]>)_variable).GetValueAsync(token));
            public async Task CaseOfUInt64Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<ulong>)_variable).GetValueAsync(token));
            public async Task CaseOfUInt64ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<ulong[]>)_variable).GetValueAsync(token));
            public async Task CaseOfNullableUInt64Async(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<ulong?>)_variable).GetValueAsync(token));
            public async Task CaseOfNullableUInt64ArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<ulong?[]>)_variable).GetValueAsync(token));
            public async Task CaseOfFloatAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<float>)_variable).GetValueAsync(token));
            public async Task CaseOfFloatArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<float[]>)_variable).GetValueAsync(token));
            public async Task CaseOfNullableFloatAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<float?>)_variable).GetValueAsync(token));
            public async Task CaseOfNullableFloatArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<float?[]>)_variable).GetValueAsync(token));
            public async Task CaseOfDoubleAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<double>)_variable).GetValueAsync(token));
            public async Task CaseOfDoubleArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<double[]>)_variable).GetValueAsync(token));
            public async Task CaseOfNullableDoubleAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<double?>)_variable).GetValueAsync(token));
            public async Task CaseOfNullableDoubleArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<double?[]>)_variable).GetValueAsync(token));
            public async Task CaseOfStringAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<string>)_variable).GetValueAsync(token));
            public async Task CaseOfStringArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<string[]>)_variable).GetValueAsync(token));
            public async Task CaseOfDateTimeAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<DateTime>)_variable).GetValueAsync(token));
            public async Task CaseOfDateTimeArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<DateTime[]>)_variable).GetValueAsync(token));
            public async Task CaseOfNullableDateTimeAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<DateTime?>)_variable).GetValueAsync(token));
            public async Task CaseOfNullableDateTimeArrayAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<DateTime?[]>)_variable).GetValueAsync(token));
            public async Task CaseOfObjectAsync(CancellationToken token) => Value = MultiTypeValue.Create(await ((IVariable<object>)_variable).GetValueAsync(token));
            public Task CaseOfErrorAsync(CancellationToken token) => throw new Exception();
        }

        ORiN3FieldDesign? _design;
        readonly Random _random = new Random();
        readonly IDictionary<string, IVariable> _variables = new Dictionary<string, IVariable>();

        private static async Task<IRootObject> WakeupProviderAsync(string host, int port, string providerId, string providerVersion, int providerPort, CancellationToken token)
        {
            using var channel = GrpcChannel.ForAddress($"http://{host}:{port}/");
            using var remoteEngine = await RemoteEngine.AttachAsync(channel, uint.MaxValue, token);

            // Launching Provider
            var providerEndpoints = new ProviderEndpoint[] { new(0, host, providerPort, []) };
            var telemetryEndpoints = Array.Empty<TelemetryEndpoint>();
            var telemetryAttributes = new Dictionary<string, string> { };
            var telemetryOption = new TelemetryOption(true, telemetryEndpoints, telemetryAttributes);
            var extensions = new Dictionary<string, string> { };
            var wakeupProviderResult = await remoteEngine.WakeupProviderAsync(
                id: providerId,
                version: providerVersion,
                threadSafeMode: true,
                endpoints: providerEndpoints,
                logLevel: ORiN3LogLevel.Information,
                telemetryOption: telemetryOption,
                extension: extensions,
                token: token);

            if (providerPort == 0)
            {
                providerPort = wakeupProviderResult.ProviderInformation.EndPoints
                    .Where(x => Uri.TryCreate(x.Uri, UriKind.Absolute, out _))
                    .Select(x => new Uri(x.Uri))
                    .First(x => x.Host == host)
                    .Port;
            }

            // Attach RootObject
            var providerChannel = GrpcChannel.ForAddress($"http://{host}:{providerPort}");
            return await ORiN3RootObject.AttachAsync(providerChannel, uint.MaxValue, token);
        }

        public async Task SetDesignAsync(ORiN3FieldDesign? design)
        {
            if (design == null || ReferenceEquals(_design, design))
            {
                return;
            }

            _design = design;

            using var cts = new CancellationTokenSource();

            // Launching Provider
            var root = await WakeupProviderAsync(design.RemoteEngineHost, design.RemoteEnginePort, design.ProviderId, design.ProviderVersion, design.ProviderPort, cts.Token);

            var parents = new Dictionary<string, IORiN3Object>();
            foreach (var it in design.ORiN3Objects)
            {
                using var setting = JsonDocument.Parse(it);
                var rootElement = setting.RootElement;
                var parent = rootElement.GetProperty("parent").GetString();
                var key = rootElement.GetProperty("key").GetString()!;
                var name = rootElement.GetProperty("name").GetString()!;
                var typeName = rootElement.GetProperty("typeName").GetString()!;
                var option = rootElement.GetProperty("option").GetString()!;
                var objectType = rootElement.GetProperty("objectType").GetString();
                var variableType = rootElement.TryGetProperty("variableType", out var prop) ? prop.GetString() : null;

                if (objectType == "Controller")
                {
                    var controller = await ((IControllerCreator)(parent == null ? root : parents[parent])).CreateControllerAsync(
                        name: name,
                        typeName: typeName,
                        option: option,
                        token: cts.Token);
                    parents.Add(name, controller);
                }
                else if (objectType == "Variable")
                {
                    if (variableType == "bool")
                    {
                        var variable = await ((IChildCreator)(parent == null ? root : parents[parent])).CreateVariableAsync<bool>(
                            name: name,
                            typeName: typeName,
                            option: option,
                            token: cts.Token);
                        _variables.Add(key, variable);
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
            }
        }

        public async Task<Dictionary<string, MultiTypeValue>> GetValuesAsync(List<string> devices)
        {
            //TODO: kakei
            var texts = new[] { "a", "b", "c", "d", "e" };

            var dic = new Dictionary<string, MultiTypeValue>();
            var branch = new CreateMultiTypeValueBranch(_variables["R1"]);
            await TypeSwitcher.ExecuteAsync(_variables["R1"].ORiN3ValueType, branch, CancellationToken.None);
            dic["R1"] = branch.Value;
            dic["D1"] = MultiTypeValue.Create(_random.Next(10000));
            dic["D2"] = MultiTypeValue.Create(texts[_random.Next(texts.Length)]);
            await Task.CompletedTask;
            return dic;
        }
    }
}
