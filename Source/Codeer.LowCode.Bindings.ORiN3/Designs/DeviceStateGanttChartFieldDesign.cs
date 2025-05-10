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
        [Designer]
        public int StartHour { get; set; } = 9;
        [Designer]
        public int EndHour { get; set; } = 18;
        [Designer]
        public int StepMin { get; set; } = 10;

        [Designer(CandidateType = CandidateType.Module)]
        public string ModuleName { get; set; } = string.Empty;

        [Designer(CandidateType = CandidateType.Field), ModuleMember(Member = $"{nameof(ModuleName)}")]
        public string TimeField { get; set; } = string.Empty;

        [Designer(CandidateType = CandidateType.Field), ModuleMember(Member = $"{nameof(ModuleName)}")]
        public string DeviceField { get; set; } = string.Empty;

        [Designer(CandidateType = CandidateType.Field), ModuleMember(Member = $"{nameof(ModuleName)}")]
        public string StateField { get; set; } = string.Empty;

        [Designer(CandidateType = CandidateType.StringList)]
        public List<string> DeviceAndMachinery { get; set; } = [];

        [Designer(CandidateType = CandidateType.StringList)]
        public List<string> StateAndTextAndColor { get; set; } = [];

        public override string GetWebComponentTypeFullName() => typeof(DeviceStateGanttChartFieldComponent).FullName!;
        public override string GetSearchWebComponentTypeFullName() => string.Empty;
        public override string GetSearchControlTypeFullName() => string.Empty;
        public override FieldBase CreateField() => new DeviceStateGanttChartField(this);
        public override FieldDataBase? CreateData() => null;
    }
}
