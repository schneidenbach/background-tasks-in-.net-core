using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BackgroundServiceExamples.Core;

public class LongRunningBackgroundProcessWithHealthCheck : BackgroundService
{
    private readonly ILogger<LongRunningBackgroundProcessWithHealthCheck> _logger;
    private readonly BackgroundServiceResultStore _resultStore;

    public LongRunningBackgroundProcessWithHealthCheck(
        ILogger<LongRunningBackgroundProcessWithHealthCheck> logger,
        BackgroundServiceResultStore resultStore
    )
    {
        _logger = logger;
        _resultStore = resultStore;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            DoStuffAsync();
            
            var random = new Random();
            var result = random.NextDouble() < 0.5
                ? new BackgroundServiceExecutionResult
                {
                    Status = BackgroundServiceExecutionResultStatus.Successful,
                    Message = "All good!",
                    Timestamp = DateTime.UtcNow
                }
                : new BackgroundServiceExecutionResult
                {
                    Status = BackgroundServiceExecutionResultStatus.Failed,
                    Message = "Something went wrong!",
                    Timestamp = DateTime.UtcNow
                };
            _resultStore.SetResult(this, result);
            
            await Task.Delay(10000, stoppingToken);
        }
    }

    public void DoStuffAsync()
    {
        _logger.LogInformation("Long Running Background Process running at: {time}", DateTimeOffset.Now);
    }
}