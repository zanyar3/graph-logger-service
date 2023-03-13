using System.Net;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace GraphLogger.Infrastructure.Middlewares;

public class GraphLoggerMiddleware 
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GraphLoggerMiddleware> _logger;

    public GraphLoggerMiddleware(RequestDelegate next, ILogger<GraphLoggerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(exception:ex, message: ex.InnerException?.Message ?? ex.Message);

            var responseData = new
            {
                error = new
                {
                    message = ex.InnerException?.Message ?? ex.Message,
                    innerError = new
                    {
                        date = DateTime.Now,
                        ticks = DateTime.UtcNow.Ticks,
                        ClientRequestId = context.TraceIdentifier,
                    }
                }
            };

            var response = context.Response;

            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var responseText = JsonConvert.SerializeObject(responseData);

            await response.WriteAsync(responseText);
        }
    }
}
