namespace GraphLogger.Infrastructure.Common.Models;

internal class SiteUptimeMonitorTrigger
{
    public string Site { get; set; }
    public string Url { get; set; }
    public bool IsUp { get; set; }
}
