using Codeer.LowCode.Bindings.ORiN3.Designs;
using Codeer.LowCode.Blazor.Repository;
using Colda.CommonUtilities.Tasks;
using Design.ORiN3.Provider.V1.Base;

namespace Codeer.LowCode.Bindings.ORiN3.Server
{
    public class ORiN3IO
    {
        ORiN3FieldDesign? _design;
        readonly Random _random = new();
        ORiN3Provider _provider;
        AsyncLock _lock = new();
        string _designFileDirector;

        public ORiN3IO(string designFileDirectory)
            => _designFileDirector = designFileDirectory;

        private static async Task<ORiN3Provider> WakeupProviderAsync(string host, int port, string providerId, string providerVersion, int providerPort, CancellationToken token)
        {
            using var remoteEngine = await ORiN3RemoteEngine.AttachAsync(host, port, token);
            return await remoteEngine.WakeupOrAttachProviderAsync(providerId, providerVersion, providerPort, token);
        }

        public async Task SetDesignAsync(ORiN3FieldDesign? design)
        {
            using (await _lock.LockAsync())
            {
                if (design == null || ReferenceEquals(_design, design))
                {
                    return;
                }

                _design = design;

                using var cts = new CancellationTokenSource();

                // Launching Provider
                _provider = await WakeupProviderAsync(design.RemoteEngineHost, design.RemoteEnginePort, design.ProviderId, design.ProviderVersion, design.ProviderPort, cts.Token);

                var parents = new Dictionary<string, IORiN3Object>();
                foreach (var it in design.ORiN3Objects)
                {
                    await _provider.CreateObjectAsync(it, cts.Token);
                }
            }
        }

        public async Task<Dictionary<string, MultiTypeValue>> GetValuesAsync(List<string> devices)
        {
            using (await _lock.LockAsync())
            {
                //TODO: kakei
                var texts = new[] { "a", "b", "c", "d", "e" };
                var dic = new Dictionary<string, MultiTypeValue>();

                if (_provider == null)
                {
                    dic["R1"] = MultiTypeValue.Create(false);
                    dic["D1"] = MultiTypeValue.Create(_random.Next(10000));
                    dic["D2"] = MultiTypeValue.Create(texts[_random.Next(texts.Length)]);
                    return dic;
                }

                dic["R1"] = await _provider.GetValueAsync("R1", CancellationToken.None);
                dic["D1"] = MultiTypeValue.Create(_random.Next(10000));
                dic["D2"] = MultiTypeValue.Create(texts[_random.Next(texts.Length)]);
                await Task.CompletedTask;
                return dic;
            }
        }
    }
}
