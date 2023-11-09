using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BackgroundServiceExamples.Core;

public class LongRunningBackgroundProcess : BackgroundService
{
    private readonly ILogger<LongRunningBackgroundProcess> _logger;

    public LongRunningBackgroundProcess(ILogger<LongRunningBackgroundProcess> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            DoStuffAsync();
            await Task.Delay(1000, stoppingToken);
        }
    }

    public void DoStuffAsync()
    {
        _logger.LogInformation("Long Running Background Process running at: {time}", DateTimeOffset.Now);
    }
}