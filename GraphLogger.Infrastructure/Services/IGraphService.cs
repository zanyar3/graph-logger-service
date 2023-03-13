namespace GraphLogger.Infrastructure.Services;

internal interface IGraphService
{
    Task LogsAsync(LogArgs args);
    Task UptimeMonitorTrigger(SiteUptimeMonitorTrigger site);
}
