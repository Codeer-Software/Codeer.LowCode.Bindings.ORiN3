using Codeer.LowCode.Bindings.ORiN3.Designs;
using Codeer.LowCode.Blazor.DataIO;
using Codeer.LowCode.Blazor.OperatingModel;
using Codeer.LowCode.Blazor.Repository;
using Codeer.LowCode.Blazor.Repository.Data;
using Codeer.LowCode.Blazor.Script;

namespace Codeer.LowCode.Bindings.ORiN3.Fields
{
    public class ORiN3MonitorField(ORiN3MonitorFieldDesign design)
        : FieldBase<ORiN3MonitorFieldDesign>(design)
    {
        public override bool IsModified => false;
        public override FieldDataBase? GetData() => null;
        public override FieldSubmitData GetSubmitData() => new();
        public override async Task SetDataAsync(FieldDataBase? fieldDataBase) => await Task.CompletedTask;

        readonly Dictionary<string, List<FieldBase>> _deviceAndFields = [];

        internal bool IsPollingTarget => _deviceAndFields.Count != 0;

        public override async Task InitializeDataAsync(FieldDataBase? fieldDataBase)
        {
            if (!Design.IsPollingEnabled)
            {
                AddTarget(this);
            }
            else
            {
                var samePollingTimes = Module.GetFields().OfType<ORiN3MonitorField>().Where(e => e.Design.PollingTime == Design.PollingTime).ToList();

                // Polling only the first one at the same polling time.
                if (samePollingTimes.First().Design.Name != Design.Name) return;

                samePollingTimes.ForEach(e => e.AddTarget(e));
            }
            await Task.CompletedTask;
        }

        private void AddTarget(ORiN3MonitorField monitorField)
        {
            foreach (var i in monitorField.Design.Items)
            {
                var sp = i.Split(',').Select(e => e.Trim()).ToArray();
                if (sp.Length != 2) continue;
                var field = Module.GetField(sp[1]);
                if (field == null) continue;

                var key = $"{monitorField.Design.SettingModule}.{monitorField.Design.ORiN3Field}.{sp[0]}";
                if (!_deviceAndFields.TryGetValue(key, out var list))
                {
                    list = [];
                    _deviceAndFields[key] = list;
                }
                list.Add(field);
            }
        }

        [ScriptName("UpdateAll")]
        public async Task UpdateAllAsync()
            => await UpdateAsyncCore([.. _deviceAndFields.Keys]);

        [ScriptName("Update")]
        public async Task UpdateAsync(params string[] devices)
            => await UpdateAsyncCore(devices.Select(e=> $"{Design.SettingModule}.{Design.ORiN3Field}.{e}").ToList());

        bool _updating;
        async Task UpdateAsyncCore(List<string> targetFullName)
        {
            if (_updating) return;
            _updating = true;
            try
            {
                var io = (IORiN3IO)Services.AppInfoService;

                var result = await io.GetValues(targetFullName);
                using var scope = Module.SuspendNotifyStateChanged();

                //TODO ; Show error message
                foreach (var e in result)
                {
                    if (_deviceAndFields.TryGetValue(e.Key, out var list))
                    {
                        foreach (var field in list)
                        {
                            try
                            {
                                await SetValue(field, e.Value.Value);
                            }
                            catch { }
                        }
                    }
                }
            }
            finally
            {
                _updating = false;
            }
        }

        //TODO : Make it extensible, and more.
        async Task SetValue(FieldBase field, MultiTypeValue value)
        {
            if (field is BooleanField booleanField) await booleanField.SetValueAsync(Convert.ToBoolean(value.GetValue()));
            else if (field is NumberField numberField) await numberField.SetValueAsync(Convert.ToDecimal(value.GetValue()));
            else if (field is TextField textField) await textField.SetValueAsync(value.GetValue()?.ToString() ?? string.Empty);
            else if (field is ValueImageField image) image.SetValue(Convert.ToDecimal(value.GetValue()));
            else if (field is MeterField meter) meter.SetValue(Convert.ToDouble(value.GetValue()));
        }
    }
}
