using Codeer.LowCode.Bindings.ORiN3.Components;
using Codeer.LowCode.Bindings.ORiN3.Fields;
using Codeer.LowCode.Blazor.OperatingModel;
using Codeer.LowCode.Blazor.Repository.Data;
using Codeer.LowCode.Blazor.Repository.Design;

namespace Codeer.LowCode.Bindings.ORiN3.Designs
{
    [ToolboxIcon(PackIconMaterialKind = "ProgressStar")]
    public class DeviceStateGanttChartFieldDesign() : FieldDesignBase(typeof(DeviceStateGanttChartFieldDesign).FullName!)
    {
        public override string GetWebComponentTypeFullName() => typeof(DeviceStateGanttChartFieldComponent).FullName!;
        public override string GetSearchWebComponentTypeFullName() => string.Empty;
        public override string GetSearchControlTypeFullName() => string.Empty;
        public override FieldBase CreateField() => new DeviceStateGanttChartField(this);
        public override FieldDataBase? CreateData() => null;
    }
}
