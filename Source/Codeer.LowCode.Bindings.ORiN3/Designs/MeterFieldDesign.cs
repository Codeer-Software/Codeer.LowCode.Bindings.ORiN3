using Codeer.LowCode.Bindings.ORiN3.Components;
using Codeer.LowCode.Bindings.ORiN3.Fields;
using Codeer.LowCode.Blazor.OperatingModel;
using Codeer.LowCode.Blazor.Repository.Data;
using Codeer.LowCode.Blazor.Repository.Design;

namespace Codeer.LowCode.Bindings.ORiN3.Designs
{
    [ToolboxIcon(PackIconMaterialKind = "ProgressStar")]
    public class MeterFieldDesign() : FieldDesignBase(typeof(MeterFieldDesign).FullName!)
    {
        //TODO percentage
        [Designer]
        public string MeterColor { get; set; } = "#ff4d4d";

        public override string GetWebComponentTypeFullName() => typeof(MeterFieldComponent).FullName!;
        public override string GetSearchWebComponentTypeFullName() => string.Empty;
        public override string GetSearchControlTypeFullName() => string.Empty;
        public override FieldBase CreateField() => new MeterField(this);
        public override FieldDataBase? CreateData() => null;
    }
}
