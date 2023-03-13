using GraphLogger.Infrastructure.Common.Enums;

namespace GraphLogger.Infrastructure.Common.Models;

public class UptimeMonitorConfig
{
    public UptimeMonitorConfig(string name, string url, CheckFrequency checkFrequency)
    {
        Name = name;
        Url = url;
        CheckFrequency = checkFrequency;
    }

    public string Name { get; set; }
    public string Url { get; set; }
    public CheckFrequency CheckFrequency { get; set; }
}
