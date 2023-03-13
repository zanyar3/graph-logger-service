using Microsoft.Extensions.Logging;

namespace GraphLogger.Infrastructure.Common.Models;

public class LogArgs
{
    public LogArgs(LogLevel logLevel, string clientRequestId)
    {
        LogLevel = logLevel;
        ClientRequestId = clientRequestId;

        Timestamp = DateTime.UtcNow;
    }

    public LogLevel LogLevel { get; set; }
    public string ClientRequestId { get; set; }
    public string Message { get; set; }
    public string StackTrace{ get; set; }
    public DateTime Timestamp { get; }
}
