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

        [Designer(DisplayName = "Polling Interval (ms)", CandidateType = CandidateType.Variable)]
        public int PollingIntervalMSec { get; set; } = 1000;
        [Designer(DisplayName = "Inactivity Timeout (s)", CandidateType = CandidateType.Variable)]
        public int InactivityTimeout { get; set; } = 60;

        [Designer(Category = "ORiN3 Configurator Setting", DisplayName = "O3 Json", CandidateType = CandidateType.Resource)]
        public string O3JsonFilePath { get; set; } = string.Empty;
        [Designer(Category = "ORiN3 Configurator Setting", DisplayName = "O3 Tree Json", CandidateType = CandidateType.Resource)]
        public string O3TreeJsonFilePath { get; set; } = string.Empty;

        public override string GetWebComponentTypeFullName() => typeof(ORiN3FieldComponent).FullName!;
        public override string GetSearchWebComponentTypeFullName() => string.Empty;
        public override string GetSearchControlTypeFullName() => string.Empty;
        public override FieldBase CreateField() => new ORiN3Field(this);
        public override FieldDataBase? CreateData() => null;
    }
}
