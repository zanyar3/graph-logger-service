using Microsoft.Extensions.Logging;

namespace GraphLogger.Infrastructure.Common.Extensions;

/// <summary>
/// Server-Side Rendering html generates
/// </summary>
internal class HtmlTemplateExtensions
{
    public static string Alert(LogArgs args)
    {
        var htmlTemplate = GetTemplate(args);
        return htmlTemplate;
    }

    private static string GetAlertTypeStyle(LogLevel loggerLevel)
    {
        return loggerLevel switch
        {
            LogLevel.Information => "background-color: #d9edf7; color: #31708f;",
            LogLevel.Warning => "background-color: #fcf8e3; color: #8a6d3b;",
            LogLevel.Error => "background-color: #f44336; color: #fff;",
            _ => throw new NotImplementedException(),
        };
    }

    private static string GetTemplate(LogArgs args)
    {
        string alertTypeCss = GetAlertTypeStyle(args.LogLevel);

        return $@"
            <div style='border: 1px solid #ccc; border-radius: 5px; box-shadow: 0 2px 2px rgba(0, 0, 0, 0.3); margin: 20px; padding: 20px; background-color: #333; color: #fff;'>
              <div style='background-color: #222; border-bottom: 1px solid #ccc; font-size: 1.2em; font-weight: bold; padding: 10px; text-align: center;'>
                <h3 style='margin: 0 0 .5rem'>App Monitor Logs</h3>
                <h5 style='margin: 0'>Request Client ID [{args.ClientRequestId}]</h5>
              </div>
              <div class='card-body'>
                <div style='border-bottom: 1px solid #ccc; {alertTypeCss}'>
                  <div style='font-size: 0.8em; font-style: italic; margin-bottom: 5px;'>
                    {args.Timestamp}
                  </div>
                  <div style='font-size: 1.1em; font-weight: bold;'>
                    <b>{args.LogLevel}:</b> {args.Message}
                  </div>
                </div>
              </div>
              <div style='background-color: #222; border-top: 1px solid #ccc;padding: 10px;font-size: 0.9em;font-style: italic;'>
                <b>Stack Track:</b> {args.StackTrace}
              </div>
            </div>".Trim();
    }

    public static string UptimeMonitor(SiteUptimeMonitorTrigger site)
    {
        return GetSiteStates(site);
    }

    private static string GetSiteStates(SiteUptimeMonitorTrigger site)
    {
        string cardColor = site.IsUp ? "background-color: #d9edf7; color: #31708f;" : "background-color: #f44336; color: #fff;";
        return $@"
                 <div style='border: 1px solid #ccc;border-radius: 5px;box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);margin-bottom: 20px; {cardColor}'>
                    <div style='border-bottom: 1px solid #ccc;padding: 10px;'>
                        <h2>{site.Site}</h2>
                    </div>
                    <div style=' padding: 20px;'>
                        <p>{site.Url}</p>
                        <p>Status: <span style='font-weight: bold;'>{(site.IsUp ? "UP" : "DOWN")}</span></p>
                        <p>Last Checked: {DateTime.Now:yyyy/MM/dd HH:mm:ss tt}</p>
                    </div>
                </div>";
    }
}
