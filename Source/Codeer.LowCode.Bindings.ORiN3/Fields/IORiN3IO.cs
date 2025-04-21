using Codeer.LowCode.Blazor.Repository;

namespace Codeer.LowCode.Bindings.ORiN3.Fields
{
    public interface IORiN3IO
    {
        Task<Dictionary<string, MultiTypeValue>> GetValues(List<string> devices);
    }
}
