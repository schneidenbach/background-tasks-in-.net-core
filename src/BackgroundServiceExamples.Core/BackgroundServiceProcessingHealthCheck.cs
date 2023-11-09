using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BackgroundServiceExamples.Core;

public class BackgroundServiceProcessingHealthCheck : IHealthCheck
{
    private readonly BackgroundServiceResultStore _resultStore;

    public BackgroundServiceProcessingHealthCheck(BackgroundServiceResultStore resultStore)
    {
        _resultStore = resultStore;
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        var allResults = _resultStore.GetAllResults();
        bool hasErrors = allResults.Any(r => r.Value.Status == BackgroundServiceExecutionResultStatus.Failed);

        if (hasErrors)
        {
            return Task.FromResult(HealthCheckResult.Unhealthy("There are failed background service executions."));
        }
        
        return Task.FromResult(HealthCheckResult.Healthy("Background services are running normally."));
    }
}