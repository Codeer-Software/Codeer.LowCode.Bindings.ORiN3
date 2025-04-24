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
        ORiN3FieldDesign? _design;
        readonly Random _random = new();
        IList<ORiN3Provider> _providers = [];
        AsyncLock _lock = new();
        string _designFileDirector;
        O3Setting _o3Setting;
        O3TreeSetting _o3TreeSetting;

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
                //TODO: kakei
                var texts = new[] { "a", "b", "c", "d", "e" };
                var dic = new Dictionary<string, MultiTypeValue>();

                if (_providers.Count() == 0)
                {
                    dic["R1"] = MultiTypeValue.Create(false);
                    dic["D1"] = MultiTypeValue.Create(_random.Next(10000));
                    dic["D2"] = MultiTypeValue.Create(texts[_random.Next(texts.Length)]);
                    return dic;
                }

                dic["R1"] = await _providers[0].GetValueAsync("R1", CancellationToken.None);
                dic["D1"] = await _providers[0].GetValueAsync("D1", CancellationToken.None);
                dic["D2"] = MultiTypeValue.Create(texts[_random.Next(texts.Length)]);
                await Task.CompletedTask;
                return dic;
            }
        }
    }
}
