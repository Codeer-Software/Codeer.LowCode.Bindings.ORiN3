using Codeer.LowCode.Blazor.SystemSettings;
using ORiN3App.Client.Shared.Services;
using ORiN3App.Server.Services.AI;
using ORiN3App.Server.Services.DataChangeHistory;
using ORiN3App.Server.Services.FileManagement;

namespace ORiN3App.Server.Services
{
    public class SystemConfig
    {
        public static SystemConfig Instance { get; set; } = new();

        public bool CanScriptDebug { get; set; }
        public bool UseHotReload { get; set; }
        public DataSource[] DataSources { get; set; } = [];
        public FileStorage[] FileStorages { get; set; } = [];
        public DataChangeHistoryTableInfo[] DataChangeHistoryTableInfo { get; set; } = [];
        public TemporaryFileTableInfo[] TemporaryFileTableInfo { get; set; } = [];
        public string DesignFileDirectory { get; set; } = string.Empty;
        public string FontFileDirectory { get; set; } = string.Empty;
        public MailSettings MailSettings { get; set; } = new();
        public AISettings AISettings { get; set; } = new();
        public SystemConfigForFront ForFront() => new SystemConfigForFront { CanScriptDebug = CanScriptDebug, UseHotReload = UseHotReload };
    }
}
