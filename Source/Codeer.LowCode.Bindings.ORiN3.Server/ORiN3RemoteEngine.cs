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
            if (remoteEngineSetting.Protocol != 0)
            {
                throw new NotSupportedException($"The specified protocol value '{remoteEngineSetting.Protocol}' is not supported. Currently, only HTTP (protocol value 0) is supported.");
            }

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
            if (rootSetting.Protocol != 0)
            {
                throw new NotSupportedException($"The specified protocol value '{rootSetting.Protocol}' is not supported. Currently, only HTTP (protocol value 0) is supported.");
            }

            var providerEndpoints = new ProviderEndpoint[] { new(rootSetting.Protocol, rootSetting.Host, rootSetting.PortAutoAllocationEnabled ? 0 : rootSetting.Port, []) };
            var telemetryEndpoints = rootSetting.TelemetryEndpoints.Select(_ => new TelemetryEndpoint(_.Uri, _.TelemetryTypeFlag, _.ProxySetting, _.ProxyAddress, _.ProtocolType, [])).ToArray();
            var telemetryOption = new TelemetryOption(rootSetting.UseRemoteEngineTelemetrySetting, telemetryEndpoints, Attributes: new Dictionary<string, string>());
            try
            {
                var wakeupProviderResult = await _remoteEngine!.WakeupProviderAsync(rootSetting.ProviderId, version: rootSetting.Version,
                    rootSetting.ThreadSafeMode, providerEndpoints, rootSetting.LogLevel, telemetryOption, rootSetting.Extension, token).ConfigureAwait(false);

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
