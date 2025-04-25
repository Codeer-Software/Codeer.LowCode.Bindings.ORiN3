using Codeer.LowCode.Blazor.Repository;

namespace Codeer.LowCode.Bindings.ORiN3.Fields
{
    public class ORiN3Value<T> : MultiTypeValue
    {
        public T? Value { get; set; }
        public ORiN3Value() : base(typeof(ORiN3Value<T>).FullName!) { }
        public ORiN3Value(T src) : base(typeof(ORiN3Value<T>).FullName!) => Value = src;
        public override object? GetValue() => Value;
    }
}
