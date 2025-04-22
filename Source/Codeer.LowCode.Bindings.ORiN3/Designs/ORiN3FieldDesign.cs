using Codeer.LowCode.Bindings.ORiN3.Components;
using Codeer.LowCode.Bindings.ORiN3.Fields;
using Codeer.LowCode.Blazor.DataIO;
using Codeer.LowCode.Blazor.OperatingModel;
using Codeer.LowCode.Blazor.Repository.Data;
using Codeer.LowCode.Blazor.Repository.Design;

namespace Codeer.LowCode.Bindings.ORiN3.Designs
{
    //public class ORiN3Field(ORiN3FieldDesign design)
    //    : FieldBase<ORiN3FieldDesign>(design)
    //{
    //    public override bool IsModified => false;
    //    public override FieldDataBase? GetData() => null;
    //    public override FieldSubmitData GetSubmitData() => new();
    //    public override async Task InitializeDataAsync(FieldDataBase? fieldDataBase) => await Task.CompletedTask;
    //    public override async Task SetDataAsync(FieldDataBase? fieldDataBase) => await Task.CompletedTask;
    //}

    [ToolboxIcon(PackIconMaterialKind = "ProgressStar")]
    public class ORiN3FieldDesign() : FieldDesignBase(typeof(ORiN3FieldDesign).FullName!)
    {
        public int PollingTime { get; set; } = 1000;

        [Designer(Category = "ORiN3 Remote Engine", DisplayName = "Remote Engine Host", CandidateType = CandidateType.Variable)]
        public string RemoteEngineHost { get; set; } = "127.0.0.1";
        [Designer(Category = "ORiN3 Remote Engine", DisplayName = "Remote Engine Port", CandidateType = CandidateType.Variable)]
        public int RemoteEnginePort { get; set; } = 7103;

        [Designer(Category = "ORiN3 Provider", DisplayName = "Provider", CandidateType = CandidateType.ScriptEvent)]
        public string Provider { get; set; } = string.Empty;

        [Designer(Category = "ORiN3 Provider", DisplayName = "Provider Id", CandidateType = CandidateType.Variable)]
        public string ProviderId { get; set; } = "643D12C8-DCFC-476C-AA15-E8CA004F48E8";
        [Designer(Category = "ORiN3 Provider", DisplayName = "Provider Version", CandidateType = CandidateType.Variable)]
        public string ProviderVersion { get; set; } = "1.0.85";
        [Designer(Category = "ORiN3 Provider", DisplayName = "Provider Port", CandidateType = CandidateType.Variable)]
        public int ProviderPort { get; set; } = 0;
        [Designer(Category = "ORiN3 Provider", DisplayName = "ORiN3 Objects", CandidateType = CandidateType.StringList)]
        public List<string> ORiN3Objects { get; set; } = new()
        {
            "{ \"parent\": null, \"objectType\": \"Controller\", \"key\": null, \"name\": \"GeneralPurposeController\", \"typeName\": \"ORiN3.Provider.ORiNConsortium.Mock.O3Object.Controller.GeneralPurposeController, ORiN3.Provider.ORiNConsortium.Mock\", \"option\": \"{\\\"@Version\\\":\\\"1.0.85\\\"}\" }",
            "{ \"parent\": \"GeneralPurposeController\", \"objectType\": \"Variable\", \"variableType\": \"bool\", \"key\": \"R1\", \"name\": \"BoolVariable\", \"typeName\": \"ORiN3.Provider.ORiNConsortium.Mock.O3Object.Variable.BoolVariable, ORiN3.Provider.ORiNConsortium.Mock\", \"option\": \"{\\\"@Version\\\":\\\"1.0.85\\\"}\" }"
        };

        [Designer(CandidateType = CandidateType.MultilineString)]
        public string SettingJson { get; set; } = string.Empty;

        public override string GetWebComponentTypeFullName() => typeof(ORiN3FieldComponent).FullName!;
        public override string GetSearchWebComponentTypeFullName() => string.Empty;
        public override string GetSearchControlTypeFullName() => string.Empty;
        public override FieldBase CreateField() => new ORiN3Field(this);
        public override FieldDataBase? CreateData() => null;
    }
}
