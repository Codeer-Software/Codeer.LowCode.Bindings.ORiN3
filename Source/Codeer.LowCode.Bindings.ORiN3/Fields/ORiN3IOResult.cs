using Codeer.LowCode.Blazor.Repository;

namespace Codeer.LowCode.Bindings.ORiN3.Fields
{
    public class ORiN3IOResult
    {
        public string Error { get; set; } = string.Empty;
        public MultiTypeValue Value { get; set; } = new NullValue();
    }
}
