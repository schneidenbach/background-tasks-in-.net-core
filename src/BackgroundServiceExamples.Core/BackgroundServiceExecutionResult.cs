namespace BackgroundServiceExamples.Core;

public class BackgroundServiceExecutionResult
{
    public BackgroundServiceExecutionResultStatus Status { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
}