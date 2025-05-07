using Codeer.LowCode.Bindings.ORiN3.Designs;
using Codeer.LowCode.Blazor.DataIO;
using Codeer.LowCode.Blazor.OperatingModel;
using Codeer.LowCode.Blazor.Repository.Data;
using Codeer.LowCode.Blazor.Script;

namespace Codeer.LowCode.Bindings.ORiN3.Fields
{
    /* ===== Data model ===== */
    public record Segment(string Color);
    public record MachineRow(string Name, List<Segment> Segments);

    public class DeviceStateGanttChartField(DeviceStateGanttChartFieldDesign design)
        : FieldBase<DeviceStateGanttChartFieldDesign>(design)
    {
        public double Value { get; private set; }

        public override bool IsModified => false;
        public override FieldDataBase? GetData() => null;
        public override FieldSubmitData GetSubmitData() => new();
        public override async Task SetDataAsync(FieldDataBase? fieldDataBase) => await Task.CompletedTask;

        [ScriptMethodToProperty("Value")]
        public void SetValue(double value)
        {
            if (Value == value) return;
            Value = value;
            NotifyStateChanged();
        }

        public override async Task InitializeDataAsync(FieldDataBase? fieldDataBase)
        {
            await Task.CompletedTask;
            Machines = CreateData();
        }

        public List<MachineRow> Machines { get; private set; } = new();

        /* ===== Sample data ===== */
        List<MachineRow> CreateData()
        {
            var random = new Random();

            var list = new List<MachineRow>();
            foreach(var line in Design.DeviceAndMachinery)
            {
                var sp = line.Split(',').Select(e => e.Trim()).ToArray();
                if (sp.Length != 2) continue;
                var name = sp[1];
                //var device = sp[0]; TODO

                var segments = new List<Segment>();

                //TODO sample Design.StartHour, Design.EndHour, Design.StepMin, Design.StateAndTextAndColor

                for (var i = Design.StartHour; i < Design.EndHour; i++)
                {
                    for (var j = 0; j < 60; j += Design.StepMin)
                    {
                        var color = "#ffffff";
                        if (Design.StateAndTextAndColor.Any())
                        {
                            var sp2 = Design.StateAndTextAndColor[random.Next(Design.StateAndTextAndColor.Count)].Split(',');
                            if (sp2.Length == 3)
                            {
                                color = sp2[2];
                            }
                        }
                        segments.Add(new Segment(color));
                    }
                }

                list.Add(new MachineRow(name, segments));
            }
            return list;
        }
    }
}
