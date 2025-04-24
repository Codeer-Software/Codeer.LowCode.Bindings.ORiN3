using Codeer.LowCode.Bindings.ORiN3.Designs;
using Codeer.LowCode.Blazor.DataIO;
using Codeer.LowCode.Blazor.OperatingModel;
using Codeer.LowCode.Blazor.Repository;
using Codeer.LowCode.Blazor.Repository.Data;

namespace Codeer.LowCode.Bindings.ORiN3.Fields
{
    public class ORiN3MonitorField(ORiN3MonitorFieldDesign design)
        : FieldBase<ORiN3MonitorFieldDesign>(design)
    {
        public override bool IsModified => false;
        public override FieldDataBase? GetData() => null;
        public override FieldSubmitData GetSubmitData() => new();
        public override async Task SetDataAsync(FieldDataBase? fieldDataBase) => await Task.CompletedTask;

        Dictionary<string, FieldBase> _deviceAndFields = new();

        public override async Task InitializeDataAsync(FieldDataBase? fieldDataBase)
        {
            foreach (var i in Design.Items)
            { 
                var sp = i.Split(',').Select(e=>e.Trim()).ToArray();
                if (sp.Length != 2) continue;
                var field = Module.GetField(sp[1]);
                if (field == null) continue;
                _deviceAndFields[sp[0]] = field;
            }

            await Task.CompletedTask;
        }

        public async Task PollingMonitor()
        {
            var io = (IORiN3IO)Services.AppInfoService;

            var result = await io.GetValues(_deviceAndFields.Keys.ToList());

            using var scope = Module.SuspendNotifyStateChanged();

            foreach(var e in result)
            {
                if (_deviceAndFields.TryGetValue(e.Key, out var field))
                {
                    try
                    {
                        await SetValue(field, e.Value.Value);
                    }
                    catch { }
                }
            }
        }

        //TODO : Make it extensible, and more.
        async Task SetValue(FieldBase field, MultiTypeValue value)
        {
            if (field is BooleanField booleanField) await booleanField.SetValueAsync(Convert.ToBoolean(value.GetValue()));
            if (field is NumberField numberField) await numberField.SetValueAsync(Convert.ToDecimal(value.GetValue()));
            if (field is TextField textField) await textField.SetValueAsync(value.GetValue()?.ToString()??string.Empty);
        }
    }
}
