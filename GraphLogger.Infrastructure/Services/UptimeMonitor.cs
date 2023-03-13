using Microsoft.Extensions.Hosting;

namespace GraphLogger.Infrastructure.Services;

internal class UptimeMonitor : BackgroundService
{
    private readonly List<UptimeMonitorConfig> _config;
    private List<System.Timers.Timer> _cron;
    private readonly HttpClient _httpClient;
    private readonly IGraphService _graphService;
    private IDictionary<string, SiteUptimeMonitorTrigger> _monitoring;
    public UptimeMonitor(IOptions<GraphLoggerConfiguration> configuration, IGraphService graphService)
    {
        _config = configuration.Value.UptimeMonitors;
        _cron = new List<System.Timers.Timer>();
        _httpClient = new HttpClient();

        _monitoring = new Dictionary<string, SiteUptimeMonitorTrigger>();
        _graphService = graphService;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (_config == null || _config.Count() == 0)
            return Task.CompletedTask;

        var sites = _config.GroupBy(a => a.Name)
            .Select(a => a.First());
        foreach (var site in sites)
        {
            Monitoring(site);
        }

        return Task.CompletedTask;
    }

    private void Monitoring(UptimeMonitorConfig config)
    {
        var interval = (int)config.CheckFrequency;
        System.Timers.Timer timer = new(interval);

        timer.Elapsed += (sender, e) => Timer_Elapsed(sender, e, new SiteUptimeMonitorTrigger()
        {
            Url = config.Url,
            Site = config.Name,
        });
        timer.Start();
        _cron.Add(timer);
    }

    private async void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e, SiteUptimeMonitorTrigger site)
    {
        site.IsUp = await IsWebsiteUptime(site.Url);
        var hasItem = _monitoring.TryGetValue(site.Url, out var value);
        if (hasItem)
        {
            if (value.IsUp == site.IsUp)
                return;

            value.IsUp = site.IsUp;

            await _graphService.UptimeMonitorTrigger(site);
        }
        else
        {
            _monitoring.Add(site.Url, site);
        }
    }

    public async Task<bool> IsWebsiteUptime(string url)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        foreach (var item in _cron)
        {
            item.Dispose();
        }

        return base.StopAsync(cancellationToken);
    }
}
