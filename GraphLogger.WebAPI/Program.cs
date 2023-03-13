using GraphLogger.Infrastructure.Common.Extensions;
using GraphLogger.Infrastructure.Common.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddGraphLogger(config =>
{
    config.ApplicationName = "Graph-Logger Demo";
    config.GraphSetting = configuration.GetSection("GraphLogger").Get<GraphLoggerSettings>();
    config.UptimeMonitors = new List<UptimeMonitorConfig>()
    {
        new UptimeMonitorConfig("My App", "http://my-app.com", GraphLogger.Infrastructure.Common.Enums.CheckFrequency.SEC_15)
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseGraphLogger();

app.Run();
