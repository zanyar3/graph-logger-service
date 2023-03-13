using Azure.Identity;

using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Models.Security;
using Microsoft.Kiota.Abstractions.Authentication;

namespace GraphLogger.Infrastructure.Services;

internal class GraphService : IGraphService
{
    private readonly GraphServiceClient _graphClient; 
    private readonly GraphSettings _settings;
    private readonly string _appName;
    public GraphService(IOptions<GraphLoggerConfiguration> configuration)
    {
        var config = configuration.Value;
        var graphSetting = config.GraphSetting;
        _settings = graphSetting.GraphSettings;
        _appName = config.ApplicationName;

        // You can get GraphServiceClient by two options

        // Option 1
        //var token = new AccessTokenAuthenticationProvider(graphSetting.AzureSetting);
        //_graphClient = new GraphServiceClient(new BaseBearerTokenAuthenticationProvider(token));

        // Option 2
        string[] scopes = { "https://graph.microsoft.com/.default" };
        ClientSecretCredential clientSecretCredential =
            new(
                graphSetting.AzureSetting.TenantId,
                graphSetting.AzureSetting.ClientId,
                graphSetting.AzureSetting.ClientSecret,
                new()
                {
                    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
                }
            );
        _graphClient = new(clientSecretCredential, scopes);
    }

    public async Task LogsAsync(LogArgs args)
    {
        string alert = HtmlTemplateExtensions.Alert(args);

        ChatMessage chatMessage = new()
        {
            Subject = $"[{_appName}] notify new logs",
            Body = new ItemBody()
            {
                Content = alert,
                ContentType = BodyType.Html
            }
        };

        await SendMessage(chatMessage);
    }

    public async Task UptimeMonitorTrigger(SiteUptimeMonitorTrigger site)
    {
        var template = HtmlTemplateExtensions.UptimeMonitor(site);
        ChatMessage chatMessage = new()
        {
            Subject = $"[{_appName}] notify site uptime",
            Body = new ItemBody()
            {
                Content = template,
                ContentType = BodyType.Html
            }
        };

        await SendMessage(chatMessage);
    }

    private async Task SendMessage(ChatMessage chatMessage)
    {
        try
        {
            var result = await _graphClient
               .Teams[_settings.TeamId]
               .Channels[_settings.ChannelId]
               .Messages.PostAsync(chatMessage);
        }
        catch (Exception ex)
        {
            var oDataError = ex as Microsoft.Graph.Models.ODataErrors.ODataError ?? throw new Exception(ex.Message);
            throw new Exception(oDataError.Error.Message);
        }
    }
}
