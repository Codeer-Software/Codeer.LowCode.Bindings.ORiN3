using Codeer.LowCode.Bindings.ORiN3.Components;
using Codeer.LowCode.Bindings.ORiN3.Fields;
using Codeer.LowCode.Blazor.OperatingModel;
using Codeer.LowCode.Blazor.Repository.Data;
using Codeer.LowCode.Blazor.Repository.Design;

namespace Codeer.LowCode.Bindings.ORiN3.Designs
{
    [ToolboxIcon(PackIconMaterialKind = "ProgressStar")]
    public class ValueImageFieldDesign() : FieldDesignBase(typeof(ValueImageFieldDesign).FullName!)
    {
        [Designer]
        public ObjectFit ObjectFit { get; set; } = ObjectFit.Contain;

        [Designer(CandidateType = CandidateType.StringList)]
        public List<string> Items { get; set; } = [];

        public override string GetWebComponentTypeFullName() => typeof(ValueImageFieldComponent).FullName!;
        public override string GetSearchWebComponentTypeFullName() => string.Empty;
        public override string GetSearchControlTypeFullName() => string.Empty;
        public override FieldBase CreateField() => new ValueImageField(this);
        public override FieldDataBase? CreateData() => null;
    }
}
