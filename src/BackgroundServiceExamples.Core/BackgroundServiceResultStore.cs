using System.Collections.Concurrent;
using Microsoft.Extensions.Hosting;

namespace BackgroundServiceExamples.Core;

public class BackgroundServiceResultStore
{
    private readonly ConcurrentDictionary<string, BackgroundServiceExecutionResult> _resultsByType = new();

    private const int MaxResults = 100;

    public void SetResult(BackgroundService backgroundService, BackgroundServiceExecutionResult result)
    {
        var key = KeyName(backgroundService);
        _resultsByType[key] = result;
    }

    private string KeyName(BackgroundService backgroundService)
    {
        return backgroundService.GetType().Name;
    }

    public Dictionary<string, BackgroundServiceExecutionResult> GetAllResults()
    {
        return _resultsByType.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }
}