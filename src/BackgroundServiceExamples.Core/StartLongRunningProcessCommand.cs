namespace BackgroundServiceExamples.Core;

public class StartLongRunningProcessCommand
{
    public Guid Id { get; set; }
}

public class LongRunningCommandResponse
{
    public Guid Id { get; set; }
}