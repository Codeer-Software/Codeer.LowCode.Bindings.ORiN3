using Codeer.LowCode.Bindings.ORiN3.Fields;
using Codeer.LowCode.Blazor.Repository.Data;
using Codeer.LowCode.Blazor.Repository.Match;
using Codeer.LowCode.Blazor.Repository;
using Codeer.LowCode.Blazor.DesignLogic;
using Codeer.LowCode.Bindings.ORiN3.Designs;
using Codeer.LowCode.Blazor.DataIO;

namespace Codeer.LowCode.Bindings.ORiN3.Server
{
    public static class DeviceStateGanttChartFieldServer
    {
        public static async Task<List<MachineRow>> CreateData(ModuleDataIO dataIO, IModuleDesigns moduleDesigns, DeviceStateGanttChartFieldRequest request)
        {
            var mod = moduleDesigns.Find(request.ModuleName);
            if (mod == null) return new List<MachineRow>();
            var design = mod.Fields.OfType<DeviceStateGanttChartFieldDesign>().FirstOrDefault(e => e.Name == request.FieldName);
            if (design == null) return new List<MachineRow>();

            var stateAndTextAndColor = design.StateAndTextAndColor.Select(e => ToStateAndTextAndColor(e)).ToList();

            //get data
            var condition = new SearchCondition
            {
                ModuleName = design.ModuleName,
                SortFieldVariable = $"{design.TimeField}.Value",
                Condition = MultiMatchCondition.And(
                new FieldValueMatchCondition { Comparison = MatchComparison.GreaterThanOrEqual, SearchTargetVariable = $"{design.TimeField}.Value", Value = MultiTypeValue.Create(request.Start) },
                new FieldValueMatchCondition { Comparison = MatchComparison.LessThan, SearchTargetVariable = $"{design.TimeField}.Value", Value = MultiTypeValue.Create(DateTime.Today.AddHours(design.EndHour)) })
            };
            var moduleData = await dataIO.GetListAsync(condition, 0);
            var deviceValues = moduleData.Items.Select(e => ToDeviceHistoryData(design, e)).GroupBy(e => e.DeviceName).ToList();

            var machineStates = new List<MachineRow>();
            foreach (var deviceAndMachinery in design.DeviceAndMachinery.Select(e => ToDeviceAndMachinery(e)))
            {
                var segments = new List<Segment>();
                foreach (var e in GetDeviceHistorySegments(deviceValues, deviceAndMachinery))
                {
                    var color = stateAndTextAndColor.FirstOrDefault(y => y.State == e.State)?.Color ?? "#ffffff";
                    segments.Add(new() { Color = color });
                }
                machineStates.Add(new() { Name = deviceAndMachinery.Machinery, Segments = segments });
            }
            return machineStates;
        }

        static List<DeviceHistoryData> GetDeviceHistorySegments(List<IGrouping<string, DeviceHistoryData>> deviceValues, DeviceAndMachinery deviceAndMachinery)
        {
            var allData = deviceValues.FirstOrDefault(e => e.Key == deviceAndMachinery.Device)?.ToList() ?? new List<DeviceHistoryData>();

            var floorTo10Minutes = allData.Select(e => new { Time = FloorTo10Minutes(e.DateTime), Data = e });

            var compression = new List<DeviceHistoryData>();
            foreach (var e in floorTo10Minutes.GroupBy(e => e.Time))
            {
                compression.Add(e.OrderByDescending(e => e.Data.State).First().Data);
            }
            return compression;
        }

        static DateTime FloorTo10Minutes(DateTime dt)
        {
            int flooredMinute = (dt.Minute / 10) * 10;
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, flooredMinute, 0, dt.Kind);
        }

        class DeviceAndMachinery
        {
            public string Device { get; set; } = string.Empty;
            public string Machinery { get; set; } = string.Empty;
        }

        static DeviceAndMachinery ToDeviceAndMachinery(string e)
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

        static StateAndTextAndColor ToStateAndTextAndColor(string e)
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

        static DeviceHistoryData ToDeviceHistoryData(DeviceStateGanttChartFieldDesign design, ModuleData e)
        {
            if (!e.Fields.TryGetValue(design.DeviceField, out var deviceName) ||
                !e.Fields.TryGetValue(design.TimeField, out var dateTime) ||
                !e.Fields.TryGetValue(design.StateField, out var state)) return new();
            return new()
            {
                DeviceName = (deviceName as TextFieldData)?.Value ?? string.Empty,
                DateTime = (dateTime as DateTimeFieldData)?.Value ?? DateTime.MinValue,
                State = (state as NumberFieldData)?.Value ?? 0,
            };
        }
    }
}
