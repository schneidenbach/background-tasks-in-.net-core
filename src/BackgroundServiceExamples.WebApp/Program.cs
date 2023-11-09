using BackgroundServiceExamples.Core;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services
    .AddHealthChecks()
    .AddCheck<BackgroundServiceProcessingHealthCheck>("Background Service Processing");
services.AddHostedService<LongRunningBackgroundProcessWithHealthCheck>();
services.AddSingleton<BackgroundServiceResultStore>();
services.AddHealthChecksUI(config =>
{
    config.AddHealthCheckEndpoint("Background Service Health Checks", "/health");
}).AddInMemoryStorage();

var app = builder.Build();

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.UseHealthChecksUI(options => options.UIPath = "/health-ui");
app.MapGet("/", () => "Hello World!");

app.Run();