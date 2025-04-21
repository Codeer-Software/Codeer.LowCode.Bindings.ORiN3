using Codeer.LowCode.Bindings.ORiN3.Components;
using Codeer.LowCode.Bindings.ORiN3.Fields;
using Codeer.LowCode.Blazor.OperatingModel;
using Codeer.LowCode.Blazor.Repository.Data;
using Codeer.LowCode.Blazor.Repository.Design;

namespace Codeer.LowCode.Bindings.ORiN3.Designs
{
    [ToolboxIcon(PackIconMaterialKind = "ProgressStar")]
    public class ORiN3FieldDesign() : FieldDesignBase(typeof(ORiN3FieldDesign).FullName!)
    {
        public int PollingTime { get; set; } = 1000;

        [Designer(CandidateType = CandidateType.MultilineString)]
        public string SettingJson { get; set; } = string.Empty;

        public override string GetWebComponentTypeFullName() => typeof(ORiN3FieldComponent).FullName!;
        public override string GetSearchWebComponentTypeFullName() => string.Empty;
        public override string GetSearchControlTypeFullName() => string.Empty;
        public override FieldBase CreateField() => new ORiN3Field(this);
        public override FieldDataBase? CreateData() => null;
    }
}
