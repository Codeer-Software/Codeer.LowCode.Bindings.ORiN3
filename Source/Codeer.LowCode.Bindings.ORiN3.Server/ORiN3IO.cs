using Codeer.LowCode.Bindings.ORiN3.Designs;
using Codeer.LowCode.Blazor.DesignLogic;
using Codeer.LowCode.Blazor.Repository;
using Colda.CommonUtilities.Tasks;
using Design.ORiN3.Provider.V1.Base;
using System.Diagnostics;
using System.Text.Json.Nodes;

namespace Codeer.LowCode.Bindings.ORiN3.Server
{
    public class ORiN3IO
    {
        private readonly AsyncLock _asyncLock = new();
        private readonly string _designFileDirector;
        private ORiN3FieldDesign? _design;
        private IList<ORiN3Provider> _providers = [];
        private O3Setting _o3Setting;
        private O3TreeSetting _o3TreeSetting;
        private IDictionary<string, MultiTypeValue> _variableBuffer = new Dictionary<string, MultiTypeValue>();
        private bool _initialized = false;
        private Task _updateBufferTask;
        private CancellationTokenSource _updateBufferTaskCancellationTokenSource;
        private int _counter;

        public ORiN3IO(string designFileDirectory)
            => _designFileDirector = designFileDirectory;

        public async Task SetDesignAsync(ORiN3FieldDesign? design)
        {
            using var cts = new CancellationTokenSource();
            using (await _asyncLock.LockAsync(cts.Token).ConfigureAwait(false))
            {
                if (design == null)
                {
                    return;
                }
                else if (_initialized && ReferenceEquals(_design, design))
                {
                    return;
                }
                _design = design;

                await InitAsync(cts.Token).ConfigureAwait(false);
            }
        }

        private async Task InitAsync(CancellationToken token)
        {
            Debug.Assert(_design != null);

            var providers = new List<ORiN3Provider>();
            try
            {
                if (_updateBufferTask !=null)
                {
                    _updateBufferTaskCancellationTokenSource.Cancel();
                    await _updateBufferTask.ConfigureAwait(false);
                    _updateBufferTaskCancellationTokenSource.Dispose();
                    _updateBufferTaskCancellationTokenSource = null!;
                    _updateBufferTask = null!;

                    foreach (var provider in _providers)
                    {
                        try
                        {
                            provider.Dispose();
                        }
                        catch
                        {
                            // nothing to do.
                        }
                    }
                }

                _initialized = false;
                _o3Setting = null!;
                _o3TreeSetting = null!;
                _providers = null!;
                _variableBuffer.Clear();

                var o3Setting = ReadO3Json(_design, _designFileDirector);
                var o3TreeSetting = ReadO3TreeJson(_design, _designFileDirector);

                _variableBuffer = CreateVariableBuffer(o3Setting);

                var remoteEngineId = o3TreeSetting.Objects[0].Id; // Remote Engineは1つしか設定できないので要素0決め打ち
                var remoteEngineSetting = o3Setting.GetRemoteEngine(remoteEngineId);
                foreach (var it in o3TreeSetting.Objects[0].Children)
                {
                    var rootSetting = o3Setting.GetRootObject(it.Id);
                    providers.Add(await WakeupOrAttachProviderAsync(remoteEngineSetting, rootSetting, token).ConfigureAwait(false));
                }
                var parents = new Dictionary<string, IORiN3Object>();
                foreach (var provider in providers)
                {
                    await provider.CreateObjectAsync(o3Setting, o3TreeSetting, token).ConfigureAwait(false);
                }

                _updateBufferTaskCancellationTokenSource = new();
                _updateBufferTask = Task.Factory.StartNew(() => UpdateBufferTask(_updateBufferTaskCancellationTokenSource.Token));

                _o3Setting = o3Setting;
                _o3TreeSetting = o3TreeSetting;
                _providers = providers;
                _initialized = true;
            }
            catch
            {
                foreach (var provider in providers)
                {
                    try
                    {
                        provider.Dispose();
                    }
                    catch
                    {
                        // nothing to do.
                    }
                }

                _initialized = false;
                _o3Setting = null!;
                _o3TreeSetting = null!;
                _providers = null!;
                _variableBuffer.Clear();

                throw;
            }
        }

        private static async Task<ORiN3Provider> WakeupOrAttachProviderAsync(O3Setting.ORiN3RemoteEngineSetting remoteEngineSetting, O3Setting.ORiN3RootObjectSetting rootSetting, CancellationToken token)
        {
            using var remoteEngine = await ORiN3RemoteEngine.AttachAsync(remoteEngineSetting, token).ConfigureAwait(false);
            return await remoteEngine.WakeupOrAttachProviderAsync(rootSetting, token).ConfigureAwait(false);
        }

        private async Task UpdateBufferTask(CancellationToken token)
        {
            try
            {
                while (true)
                {
                    await Task.Delay(_design!.PollingIntervalMSec, token).ConfigureAwait(false);
                    using (await _asyncLock.LockAsync(token).ConfigureAwait(false))
                    {
                        ++_counter;
                        Debug.WriteLine(_counter);
                        if (_design.InactivityTimeout * 1000 < _counter * _design.PollingIntervalMSec)
                        {
                            _updateBufferTaskCancellationTokenSource.Dispose();
                            _updateBufferTaskCancellationTokenSource = null!;
                            _updateBufferTask = null!;

                            foreach (var provider in _providers)
                            {
                                try
                                {
                                    provider.Dispose();
                                }
                                catch
                                {
                                    // nothing to do.
                                }
                            }

                            _initialized = false;
                            _o3Setting = null!;
                            _o3TreeSetting = null!;
                            _providers = null!;
                            _variableBuffer.Clear();
                            return;
                        }

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
                            catch (TaskCanceledException)
                            {
                                return;
                            }
                            catch
                            {
                                // nothing to do.
                            }
                        }
                    }
                }
            }
            catch (TaskCanceledException)
            {
                // nothing to do.
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private static IDictionary<string, MultiTypeValue> CreateVariableBuffer(O3Setting o3Setting)
        {
            var variableBuffer = new Dictionary<string, MultiTypeValue>();
            foreach (var variable in o3Setting.Variables)
            {
                variableBuffer.Add(variable.Name, MultiTypeValue.Create(null));
            }
            return variableBuffer;
        }

        private static O3Setting ReadO3Json(ORiN3FieldDesign design, string designFileDirector)
        {
            using var o3File = DesignDataFileManager.GetResource(designFileDirector, design.O3JsonFilePath);
            o3File!.Position = 0;
            var jsonNode = JsonNode.Parse(o3File);
            return new O3Setting(jsonNode!);
        }

        private static O3TreeSetting ReadO3TreeJson(ORiN3FieldDesign design, string designFileDirector)
        {
            using var o3TreeFile = DesignDataFileManager.GetResource(designFileDirector, design.O3TreeJsonFilePath);
            o3TreeFile!.Position = 0;
            var jsonNode = JsonNode.Parse(o3TreeFile);
            return new O3TreeSetting(jsonNode!);
        }

        public async Task<Dictionary<string, MultiTypeValue>> GetValuesAsync(List<string> devices)
        {
            using (await _asyncLock.LockAsync())
            {
                Debug.Assert(_initialized);
                _counter = 0;

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
