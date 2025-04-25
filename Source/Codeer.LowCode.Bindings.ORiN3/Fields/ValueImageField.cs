using Codeer.LowCode.Bindings.ORiN3.Designs;
using Codeer.LowCode.Blazor.DataIO;
using Codeer.LowCode.Blazor.OperatingModel;
using Codeer.LowCode.Blazor.Repository.Data;
using Codeer.LowCode.Blazor.Script;

namespace Codeer.LowCode.Bindings.ORiN3.Fields
{
    public class ValueImageField : FieldBase<ValueImageFieldDesign>
    {
        private string _fileName = string.Empty;
        public ValueImageField(ValueImageFieldDesign design) : base(design) { }

        [ScriptHide]
        public override bool IsModified => false;
        [ScriptHide]
        public override FieldDataBase? GetData() => null;
        [ScriptHide]
        public override FieldSubmitData GetSubmitData() => new();

        [ScriptHide]
        public override async Task SetDataAsync(FieldDataBase? fieldDataBase) => await Task.CompletedTask;

        public string Base64Data => _imageData.TryGetValue(Value, out var base64) ? base64 : string.Empty;

        public string ImageExtension => _imageExtensions.TryGetValue(Value, out var ext) ? ext : string.Empty;

        Dictionary<decimal, string> _imageData = new();
        Dictionary<decimal, string> _imageExtensions = new();

        public decimal Value { get; private set; }

        [ScriptHide]
        public override async Task InitializeDataAsync(FieldDataBase? fieldDataBase)
        {
            foreach(var e in Design.Items)
            {
                var sp = e.Split(',').Select(x=>x.Trim()).ToArray();
                if (sp.Length != 2) continue;
                if (!decimal.TryParse(sp[0], out var key)) continue;

                using var memoryStream = await Services.AppInfoService.GetResourceAsync(sp[1]);
                if (memoryStream == null) return;

                var bin = memoryStream!.ToArray();
                _imageData[key] = Convert.ToBase64String(bin);
                _imageExtensions[key] = sp[1].Split('.').Last();
            }
        }

        internal void SetValue(decimal value)
        { 
            if (Value == value) return;
            Value = value;
            NotifyStateChanged();
        }
    }
}
