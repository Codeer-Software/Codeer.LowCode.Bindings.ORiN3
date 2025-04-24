using Design.ORiN3.Provider.Core.V1.Telemetry;
using Design.ORiN3.RemoteEngineEx.V1;
using Grpc.Net.Client;
using Message.Client.ORiN3.Provider.V1;
using Message.Client.ORiN3.RemoteEngine.V1;
using System.Diagnostics;

namespace Codeer.LowCode.Bindings.ORiN3.Server
{
    internal class ORiN3RemoteEngine : IDisposable
    {
        private readonly O3Setting.ORiN3RemoteEngineSetting _remoteEngineSetting;
        private readonly int _port;
        private IRemoteEngineEx? _remoteEngine;
        private bool _disposedValue;

        private ORiN3RemoteEngine(O3Setting.ORiN3RemoteEngineSetting remoteEngineSetting, IRemoteEngineEx remoteEngine)
        {
            _remoteEngineSetting = remoteEngineSetting;
            _remoteEngine = remoteEngine;
        }

        ~ORiN3RemoteEngine()
        {
            Debug.Assert(false);
            Dispose(disposing: false);
        }

        internal async static Task<ORiN3RemoteEngine> AttachAsync(O3Setting.ORiN3RemoteEngineSetting remoteEngineSetting, CancellationToken token)
        {
            var channel = GrpcChannel.ForAddress($"http://{remoteEngineSetting.Host}:{remoteEngineSetting.Port}/");
            var remoteEngine = await RemoteEngine.AttachAsync(channel, uint.MaxValue, token).ConfigureAwait(false);
            return new ORiN3RemoteEngine(remoteEngineSetting, remoteEngine);
        }

        internal async Task<ORiN3Provider> WakeupOrAttachProviderAsync(O3Setting.ORiN3RootObjectSetting rootSetting, CancellationToken token)
        {
            if (rootSetting.PortAutoAllocationEnabled)
            {
                return await WakeupProviderAsync(rootSetting, token).ConfigureAwait(false);
            }
            else
            {
                await foreach (var it in _remoteEngine!.ListProviderProcessAsync(nameFilter: string.Empty, token))
                {
                    var endpoints = it.ActualEndPoints.Select(_ => new Uri(_));
                    if (endpoints.Select(_ => _.Port).Contains(rootSetting.Port))
                    {
                        return await AttachProviderAsync(endpoints.Where(_ => _.Port == rootSetting.Port).Single(), rootSetting, instanceCreated: false, token).ConfigureAwait(false);
                    }
                }

                return await WakeupProviderAsync(rootSetting, token).ConfigureAwait(false);
             }
        }

        private Task<ORiN3Provider> AttachProviderAsync(Uri endpoint, O3Setting.ORiN3RootObjectSetting rootSetting, bool instanceCreated, CancellationToken token)
        {
            return AttachProviderAsync(GrpcChannel.ForAddress(endpoint), rootSetting, instanceCreated, token);
        }

        private Task<ORiN3Provider> AttachProviderAsync(string host, int providerPort, O3Setting.ORiN3RootObjectSetting rootSetting, bool instanceCreated, CancellationToken token)
        {
            return AttachProviderAsync(GrpcChannel.ForAddress($"http://{host}:{providerPort}"), rootSetting, instanceCreated, token);
        }

        private async Task<ORiN3Provider> AttachProviderAsync(GrpcChannel channel, O3Setting.ORiN3RootObjectSetting rootSetting, bool instanceCreated, CancellationToken token)
        {
            return new ORiN3Provider(_remoteEngineSetting.Id, await ORiN3RootObject.AttachAsync(channel, uint.MaxValue, token), rootSetting, instanceCreated);
        }

        internal async Task<ORiN3Provider> WakeupProviderAsync(O3Setting.ORiN3RootObjectSetting rootSetting, CancellationToken token)
        {
            // Launching Provider
            var providerEndpoints = new ProviderEndpoint[] { new(0, rootSetting.Host, rootSetting.PortAutoAllocationEnabled ? 0 : rootSetting.Port, []) };
            var telemetryEndpoints = Array.Empty<TelemetryEndpoint>();
            var telemetryAttributes = new Dictionary<string, string> { };
            var telemetryOption = new TelemetryOption(true, telemetryEndpoints, telemetryAttributes);
            var extensions = new Dictionary<string, string> { };
            try
            {
                var wakeupProviderResult = await _remoteEngine!.WakeupProviderAsync(
                    id: rootSetting.ProviderId,
                    version: rootSetting.Version,
                    threadSafeMode: true,
                    endpoints: providerEndpoints,
                    logLevel: ORiN3LogLevel.Information,
                    telemetryOption: telemetryOption,
                    extension: extensions,
                    token: token).ConfigureAwait(false);

                var providerPort = rootSetting.Port;
                if (rootSetting.PortAutoAllocationEnabled)
                {
                    providerPort = wakeupProviderResult.ProviderInformation.EndPoints
                        .Where(x => Uri.TryCreate(x.Uri, UriKind.Absolute, out _))
                        .Select(x => new Uri(x.Uri))
                        .First(x => x.Host == rootSetting.Host)
                        .Port;
                }

                return await AttachProviderAsync(rootSetting.Host, providerPort, rootSetting, instanceCreated: true, token);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to wakeup provider. {ex.Message}", ex);
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
                    _remoteEngine!.Dispose();
                    _remoteEngine = null;
                }
                _disposedValue = true;
            }
        }
    }
}
