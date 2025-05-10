using Codeer.LowCode.Bindings.ORiN3.Designs;
using Codeer.LowCode.Blazor.DataIO;
using Codeer.LowCode.Blazor.OperatingModel;
using Codeer.LowCode.Blazor.Repository;
using Codeer.LowCode.Blazor.Repository.Data;
using Codeer.LowCode.Blazor.Repository.Match;
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
            Machines = await CreateData();
        }

        public List<MachineRow> Machines { get; private set; } = new();

        async Task<List<MachineRow>>CreateData()
        {
            var stateAndTextAndColor = Design.StateAndTextAndColor.Select(e => ToStateAndTextAndColor(e)).ToList();

            //get data
            var condition = new SearchCondition
            {
                ModuleName = Design.ModuleName,
                SortFieldVariable = $"{Design.TimeField}.Value",
                Condition = MultiMatchCondition.And(
                new FieldValueMatchCondition { Comparison = MatchComparison.GreaterThanOrEqual, SearchTargetVariable = $"{Design.TimeField}.Value", Value = MultiTypeValue.Create(DateTime.Today.AddHours(Design.StartHour)) },
                new FieldValueMatchCondition { Comparison = MatchComparison.LessThan, SearchTargetVariable = $"{Design.TimeField}.Value", Value = MultiTypeValue.Create(DateTime.Today.AddHours(Design.EndHour)) })
            };
            var moduleData = await Services.ModuleDataService.GetListAsync(condition, 0);
            var deviceValues = moduleData.Items.Select(e => ToDeviceHistoryData(e)).GroupBy(e => e.DeviceName).ToList();

            var machineStates = new List<MachineRow>();
            foreach(var deviceAndMachinery in Design.DeviceAndMachinery.Select(e=>ToDeviceAndMachinery(e)))
            {
                var segments = new List<Segment>();
                foreach (var e in deviceValues.FirstOrDefault(e => e.Key == deviceAndMachinery.Device)?.ToList() ?? new List<DeviceHistoryData>())
                {
                    var color = stateAndTextAndColor.FirstOrDefault(y => y.State == e.State)?.Color?? "#ffffff";
                    segments.Add(new Segment(color));
                }
                machineStates.Add(new MachineRow(deviceAndMachinery.Machinery, segments));
            }
            return machineStates;
        }

        class DeviceAndMachinery
        {
            public string Device { get; set; } = string.Empty;
            public string Machinery { get; set; } = string.Empty;
        }

        DeviceAndMachinery ToDeviceAndMachinery(string e)
        {
            var sp = e.Split(',');
            if (sp.Length != 2) return new DeviceAndMachinery();
            return new DeviceAndMachinery
            {
                Device = sp[0],
                Machinery = sp[1]
            };
        }

        class StateAndTextAndColor
        {
            public decimal State { get; set; }
            public string Text { get; set; } = string.Empty;
            public string Color { get; set; } = string.Empty;
        }

        StateAndTextAndColor ToStateAndTextAndColor(string e)
        {
            var sp = e.Split(',');
            if (sp.Length != 3) return new StateAndTextAndColor();
            return new StateAndTextAndColor
            {
                State = decimal.TryParse(sp[0], out var state) ? state : 0,
                Text = sp[1],
                Color = sp[2]
            };
        }

        class DeviceHistoryData
        {
            public string DeviceName { get; set; } = string.Empty;
            public DateTime DateTime { get; set; }
            public decimal State { get; set; }
        }

        DeviceHistoryData ToDeviceHistoryData(ModuleData e)
        {
            if (!e.Fields.TryGetValue(Design.DeviceField, out var deviceName) ||
                !e.Fields.TryGetValue(Design.TimeField, out var dateTime) ||
                !e.Fields.TryGetValue(Design.StateField, out var state)) return new();
            return new()
            {
                DeviceName = (deviceName as TextFieldData)?.Value??string.Empty,
                DateTime = (dateTime as DateTimeFieldData)?.Value?? DateTime.MinValue,
                State = (state as NumberFieldData)?.Value ?? 0,
            };
        }
    }
}
