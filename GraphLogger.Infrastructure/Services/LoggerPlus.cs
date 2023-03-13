using System.Diagnostics;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GraphLogger.Infrastructure.Services;

internal class LoggerPlusProvider : ILoggerProvider
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public LoggerPlusProvider(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public ILogger CreateLogger(string categoryName)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var graphService = scope.ServiceProvider.GetService<IGraphService>();
        var httpContextAccessor = scope.ServiceProvider.GetService<IHttpContextAccessor>();

        return new LoggerPlus(categoryName, graphService, httpContextAccessor);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}

internal class LoggerPlus : ILogger
{
    private readonly string _categoryName;
    private readonly IGraphService _graphService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LoggerPlus(string categoryName, IGraphService graphService, IHttpContextAccessor httpContextAccessor)
    {
        _categoryName = categoryName;
        _graphService = graphService;
        _httpContextAccessor = httpContextAccessor;
    }

    public IDisposable BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel >= LogLevel.Information;
    }

    public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel))
            return;

        string stackTrace = exception == null ? null : exception.StackTrace?.Split('\n')[0];

        var logArgs = new LogArgs(logLevel, _httpContextAccessor.HttpContext?.TraceIdentifier ?? "")
        {
            StackTrace = stackTrace,
            Message = formatter(state, exception)
        };

       await _graphService.LogsAsync(logArgs);
    }
}
