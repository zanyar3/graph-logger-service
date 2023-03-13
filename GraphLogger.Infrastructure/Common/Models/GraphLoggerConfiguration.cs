namespace GraphLogger.Infrastructure.Common.Models;

public class GraphLoggerConfiguration
{
    public string ApplicationName { get; set; }
    public GraphLoggerSettings GraphSetting { get; set; }
    public List<UptimeMonitorConfig> UptimeMonitors { get; set; }
}