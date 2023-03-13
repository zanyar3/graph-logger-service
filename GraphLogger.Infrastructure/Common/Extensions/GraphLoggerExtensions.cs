using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace GraphLogger.Infrastructure.Common.Extensions;

public static class GraphLoggerExtensions
{
    /// <summary>
    /// Register the GraphLogger services with config
    /// </summary>
    public static IServiceCollection AddGraphLogger(
        this IServiceCollection services, 
        Action<GraphLoggerConfiguration> setupAction)
    {
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.ConfigureGraphLogger(setupAction);
        services.AddSingleton<IGraphService, GraphService>();
        services.AddSingleton<ILoggerProvider>(sp => new LoggerPlusProvider(sp.GetRequiredService<IServiceScopeFactory>()));

        services.AddHostedService<UptimeMonitor>();

        return services;
    }

    private static void ConfigureGraphLogger(
            this IServiceCollection services,
            Action<GraphLoggerConfiguration> setupAction)
    {
        services.Configure(setupAction);
    }

    /// <summary>
    /// Register the GraphLogger middleware
    /// </summary>
    public static IApplicationBuilder UseGraphLogger(this IApplicationBuilder app)
    {
        return app.UseMiddleware<GraphLoggerMiddleware>();
    }

}
