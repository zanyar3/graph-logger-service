namespace GraphLogger.Infrastructure.Common.Models;

public class GraphLoggerSettings
{
    public AzureAD AzureSetting { get; set; }
    public GraphSettings GraphSettings { get; set; }
}

public class AzureAD
{
    public string AccessToken { get; set; }

    public string ClientId { get; set; }

    public string TenantId { get; set; }

    public string ClientSecret { get; set; }
}

public class GraphSettings
{
    public string TeamId { get; set; }

    public string TeamTenantId { get; set; }

    public string ChannelId { get; set; }

    public string ChannelTenantId { get; set; }
}
