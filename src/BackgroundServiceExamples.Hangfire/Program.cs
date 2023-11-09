using BackgroundServiceExamples.Core;
using BackgroundServiceExamples.WindowsService;
using Hangfire;
using Hangfire.MemoryStorage;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddHangfire(x =>
{
    x.UseMemoryStorage();
});
services.AddHangfireServer();
services.AddTransient<LongRunningBackgroundProcess>();

var app = builder.Build();

app.UseHangfireDashboard(pathMatch: "");

RecurringJob.AddOrUpdate(
    "Something Happened",
    () => Console.WriteLine("Something happened!"),
    Cron.Minutely);

RecurringJob.AddOrUpdate(
    "Long Running Background Process",
    () => app.Services.GetRequiredService<LongRunningBackgroundProcess>().DoStuffAsync(),
    Cron.Minutely
);

app.Run();