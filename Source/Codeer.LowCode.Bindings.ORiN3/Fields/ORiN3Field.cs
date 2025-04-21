using Codeer.LowCode.Bindings.ORiN3.Designs;
using Codeer.LowCode.Blazor.DataIO;
using Codeer.LowCode.Blazor.OperatingModel;
using Codeer.LowCode.Blazor.Repository.Data;

namespace Codeer.LowCode.Bindings.ORiN3.Fields
{
    public class ORiN3Field(ORiN3FieldDesign design)
        : FieldBase<ORiN3FieldDesign>(design)
    {
        public override bool IsModified => false;
        public override FieldDataBase? GetData() => null;
        public override FieldSubmitData GetSubmitData() => new();
        public override async Task InitializeDataAsync(FieldDataBase? fieldDataBase) => await Task.CompletedTask;
        public override async Task SetDataAsync(FieldDataBase? fieldDataBase) => await Task.CompletedTask;
    }
}
