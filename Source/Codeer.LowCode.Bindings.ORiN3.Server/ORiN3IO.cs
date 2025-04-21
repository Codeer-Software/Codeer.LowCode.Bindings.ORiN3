using Codeer.LowCode.Bindings.ORiN3.Designs;
using Codeer.LowCode.Blazor.Repository;
using Design.ORiN3.Common.V1;
using Design.ORiN3.Provider.Core.V1.Telemetry;
using Design.ORiN3.Provider.V1;
using Design.ORiN3.RemoteEngineEx.V1;
using Grpc.Net.Client;
using Message.Client.ORiN3.Provider.V1;
using Message.Client.ORiN3.RemoteEngine.V1;
using Message.ORiN3.Provider.V1.Branch.Switcher;

namespace Codeer.LowCode.Bindings.ORiN3.Server
{
    public class ORiN3IO
    {
        internal class CreateMultiTypeValueBranch : IValueTypeBranchAsync
        {
            private readonly IVariable _variable;

            public MultiTypeValue Value { get; private set; }

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

        private static async Task<IRemoteEngineEx> ConnectToRemoteEngineAsync(string endpoint, CancellationToken token)
        {
            var channel = GrpcChannel.ForAddress(endpoint);
            return await RemoteEngine.AttachAsync(channel, uint.MaxValue, token);
        }

        private static async Task<IRootObject> WakeupProviderAsync(IRemoteEngineEx remoteEngine, CancellationToken token)
        {
            // Launching Provider
            var providerEndpoints = new ProviderEndpoint[] { new(0, "127.0.0.1", 0, []) };
            var telemetryEndpoints = new TelemetryEndpoint[] { };
            var telemetryAttributes = new Dictionary<string, string> { };
            var telemetryOption = new TelemetryOption(true, telemetryEndpoints, telemetryAttributes);
            var extensions = new Dictionary<string, string> { };
            var wakeupProviderResult = await remoteEngine.WakeupProviderAsync(
                id: "643D12C8-DCFC-476C-AA15-E8CA004F48E8",
                version: "1.0.85",
                threadSafeMode: true,
                endpoints: providerEndpoints,
                logLevel: ORiN3LogLevel.Information,
                telemetryOption: telemetryOption,
                extension: extensions,
                token: token);

            var providerPort = wakeupProviderResult.ProviderInformation.EndPoints
                .Where(x => Uri.TryCreate(x.Uri, UriKind.Absolute, out _))
                .Select(x => new Uri(x.Uri))
                .First(x => x.Host == "127.0.0.1")
                .Port;

            // Attach RootObject
            var providerChannel = GrpcChannel.ForAddress($"http://127.0.0.1:{providerPort}");
            return await ORiN3RootObject.AttachAsync(providerChannel, uint.MaxValue, token);
        }

        public async Task SetDesignAsync(ORiN3FieldDesign? design)
        {
            if (ReferenceEquals(_design, design))
            {
                return;
            }

            _design = design;

            using var cts = new CancellationTokenSource();

            // Connect to Remote Engine
            using var remoteEngine = await ConnectToRemoteEngineAsync("http://127.0.0.1:7103/", cts.Token);

            // Launching Provider
            var root = await WakeupProviderAsync(remoteEngine, cts.Token);

            // Create object
            var controller = await root.CreateControllerAsync(
                name: "GeneralPurposeController",
                typeName: "ORiN3.Provider.ORiNConsortium.Mock.O3Object.Controller.GeneralPurposeController, ORiN3.Provider.ORiNConsortium.Mock",
                option: "{\"@Version\":\"1.0.85\"}",
                token: cts.Token);

            // Create object
            var variable = await controller.CreateVariableAsync<bool>(
                name: "BoolVariable",
                typeName: "ORiN3.Provider.ORiNConsortium.Mock.O3Object.Variable.BoolVariable, ORiN3.Provider.ORiNConsortium.Mock",
                option: "{\"@Version\":\"1.0.85\"}",
                token: cts.Token);

            _variables["R1"] = variable;
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
