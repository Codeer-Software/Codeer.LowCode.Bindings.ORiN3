using Design.ORiN3.Provider.Core.V1.Telemetry;
using Design.ORiN3.Provider.V1;
using Design.ORiN3.RemoteEngineEx.V1;
using Grpc.Net.Client;
using Message.Client.ORiN3.Provider.V1;
using Message.Client.ORiN3.RemoteEngine.V1;
using System.Diagnostics;

namespace Codeer.LowCode.Bindings.ORiN3.Server
{
    internal class ORiN3RemoteEngine : IDisposable
    {
        private readonly string _host;
        private readonly int _port;
        private IRemoteEngineEx? _remoteEngine;
        private bool _disposedValue;

        private ORiN3RemoteEngine(string host, int port, IRemoteEngineEx remoteEngine)
        {
            _host = host;
            _port = port;
            _remoteEngine = remoteEngine;
        }

        ~ORiN3RemoteEngine()
        {
            Debug.Assert(false);
            Dispose(disposing: false);
        }

        internal async static Task<ORiN3RemoteEngine> AttachAsync(string host, int port, CancellationToken token)
        {
            var channel = GrpcChannel.ForAddress($"http://{host}:{port}/");
            var remoteEngine = await RemoteEngine.AttachAsync(channel, uint.MaxValue, token);
            return new ORiN3RemoteEngine(host, port, remoteEngine);
        }

        private static bool IsAutoAssignedPort(int port)
        {
            return port == 0;
        }

        internal async Task<ORiN3Provider> WakeupOrAttachProviderAsync(string providerId, string providerVersion, int providerPort, CancellationToken token)
        {
            if (IsAutoAssignedPort(providerPort))
            {
                return await WakeupProviderAsync(providerId, providerVersion, 0, token);
            }
            else
            {
                await foreach (var it in _remoteEngine!.ListProviderProcessAsync(" ", token))
                {
                    var endpoints = it.ActualEndPoints.Select(_ => new Uri(_));
                    if (endpoints.Select(_ => _.Port).Contains(providerPort))
                    {
                        return await AttachProviderAsync(endpoints.Where(_ => _.Port == providerPort).Single(), shutdonwWhenDisposing: false, token);
                    }
                }

                return await WakeupProviderAsync(providerId, providerVersion, providerPort, token);
             }
        }

        private Task<ORiN3Provider> AttachProviderAsync(Uri endpoint, bool shutdonwWhenDisposing, CancellationToken token)
        {
            return AttachProviderAsync(GrpcChannel.ForAddress(endpoint), shutdonwWhenDisposing, token);
        }

        private Task<ORiN3Provider> AttachProviderAsync(int providerPort, bool shutdonwWhenDisposing, CancellationToken token)
        {
            return AttachProviderAsync(GrpcChannel.ForAddress($"http://{_host}:{providerPort}"), shutdonwWhenDisposing, token);
        }

        private async Task<ORiN3Provider> AttachProviderAsync(GrpcChannel channel, bool shutdonwWhenDisposing, CancellationToken token)
        {
            return new ORiN3Provider(await ORiN3RootObject.AttachAsync(channel, uint.MaxValue, token), shutdonwWhenDisposing);
        }

        internal async Task<ORiN3Provider> WakeupProviderAsync(string providerId, string providerVersion, int providerPort, CancellationToken token)
        {
            // Launching Provider
            var providerEndpoints = new ProviderEndpoint[] { new(0, _host, providerPort, []) };
            var telemetryEndpoints = Array.Empty<TelemetryEndpoint>();
            var telemetryAttributes = new Dictionary<string, string> { };
            var telemetryOption = new TelemetryOption(true, telemetryEndpoints, telemetryAttributes);
            var extensions = new Dictionary<string, string> { };
            var wakeupProviderResult = await _remoteEngine!.WakeupProviderAsync(
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
                    .First(x => x.Host == _host)
                    .Port;
            }

            return await AttachProviderAsync(providerPort, shutdonwWhenDisposing: true, token);
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
