using Codeer.LowCode.Bindings.ORiN3.Fields;
using Codeer.LowCode.Blazor.OperatingModel;
using Codeer.LowCode.Blazor.Repository.Data;
using Codeer.LowCode.Blazor.Repository.Design;
using Codeer.LowCode.Bindings.ORiN3.Components;

namespace Codeer.LowCode.Bindings.ORiN3.Designs
{
    [ToolboxIcon(PackIconMaterialKind = "ProgressStar")]
    public class ORiN3MonitorFieldDesign() : FieldDesignBase(typeof(ORiN3MonitorFieldDesign).FullName!)
    {
        [Designer]
        public bool IsPollingEnabled { get; set; } = true;
        [Designer]
        public int PollingTime { get; set; } = 1000;

        [Designer(CandidateType = CandidateType.Module)]
        public string SettingModule { get; set; } = string.Empty;

        [Designer(CandidateType = CandidateType.Field), ModuleMember(Member = nameof(SettingModule)), TargetFieldType(Types = [typeof(ORiN3FieldDesign)])]
        public string ORiN3Field { get; set; } = string.Empty;

        // TODO : Change the typef
        [Designer(CandidateType = CandidateType.StringList)]
        public List<string> Items { get; set; } = [];

        public override string GetWebComponentTypeFullName() => typeof(ORiN3MonitorFieldComponent).FullName!;
        public override string GetSearchWebComponentTypeFullName() => string.Empty;
        public override string GetSearchControlTypeFullName() => string.Empty;
        public override FieldBase CreateField() => new ORiN3MonitorField(this);
        public override FieldDataBase? CreateData() => null;
    }
}
