using Codeer.LowCode.Bindings.ORiN3.Designs;
using Codeer.LowCode.Blazor.DataIO;
using Codeer.LowCode.Blazor.OperatingModel;
using Codeer.LowCode.Blazor.Repository.Data;
using Codeer.LowCode.Blazor.Script;

namespace Codeer.LowCode.Bindings.ORiN3.Fields
{
    public class Segment
    {
        public string Color { get; set; } = string.Empty;
    }
    public class MachineRow
    {
        public string Name { get; set; } = string.Empty;
        public List<Segment> Segments { get; set; } = new();
    }

    public class DeviceStateGanttChartFieldRequest
    {
        public string ModuleName { get; set; } = string.Empty;
        public string FieldName { get; set; } = string.Empty;
        public DateTime Start { get; set; }
    }

    public class DeviceStateGanttChartField(DeviceStateGanttChartFieldDesign design)
        : FieldBase<DeviceStateGanttChartFieldDesign>(design)
    {
        public double Value { get; private set; }

        public override bool IsModified => false;
        public override FieldDataBase? GetData() => null;
        public override FieldSubmitData GetSubmitData() => new();
        public override async Task SetDataAsync(FieldDataBase? fieldDataBase) => await Task.CompletedTask;

        [ScriptMethodToProperty("Value")]
        public void SetValue(double value)
        {
            if (Value == value) return;
            Value = value;
            NotifyStateChanged();
        }

        public override async Task InitializeDataAsync(FieldDataBase? fieldDataBase)
        {
            if (Services.AppInfoService.IsDesignMode) return;

            var io = (IORiN3IO)Services.AppInfoService;
            Machines = await io.GetDeviceStateGanttChartFieldDataAsync(
                new() 
                {
                    ModuleName = Module.Design.Name,
                    FieldName = Design.Name,
                    Start = DateTime.Today.AddHours(Design.StartHour)
                });
        }

        public List<MachineRow> Machines { get; private set; } = new();
    }
}
