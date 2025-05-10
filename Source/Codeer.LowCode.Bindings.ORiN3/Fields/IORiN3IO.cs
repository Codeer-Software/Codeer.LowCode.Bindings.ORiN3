namespace Codeer.LowCode.Bindings.ORiN3.Fields
{
    public interface IORiN3IO
    {
        Task<Dictionary<string, ORiN3IOResult>> GetValues(List<string> devices);
        Task<List<MachineRow>> GetDeviceStateGanttChartFieldDataAsync(DeviceStateGanttChartFieldRequest reqest);
    }
}
