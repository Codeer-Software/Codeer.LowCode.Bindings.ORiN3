using Codeer.LowCode.Bindings.ORiN3.Designs;
using Codeer.LowCode.Blazor.Repository;

namespace Codeer.LowCode.Bindings.ORiN3.Server
{
    public class ORiN3IO
    {
        ORiN3FieldDesign? _design;
        readonly Random _random = new Random();

        public void SetDesign(ORiN3FieldDesign? design)
        {
            if (ReferenceEquals(_design, design)) return;
            _design = design;
        }

        public async Task<Dictionary<string, MultiTypeValue>> GetValuesAsync(List<string> devices)
        {
            //TODO: kakei
            var texts = new[] { "a", "b", "c", "d", "e" };

            var dic = new Dictionary<string, MultiTypeValue>();
            dic["R1"] = MultiTypeValue.Create(_random.Next(2));
            dic["D1"] = MultiTypeValue.Create(_random.Next(10000));
            dic["D2"] = MultiTypeValue.Create(texts[_random.Next(texts.Length)]);
            await Task.CompletedTask;
            return dic;
        }
    }
}
