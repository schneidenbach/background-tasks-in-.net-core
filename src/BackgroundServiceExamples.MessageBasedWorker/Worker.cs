using BackgroundServiceExamples.Core;
using MassTransit;

namespace BackgroundServiceExamples.MessageBasedWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }
}

public class StartLongRunningProcessCommandConsumer : IConsumer<StartLongRunningProcessCommand>
{
    public async Task Consume(ConsumeContext<StartLongRunningProcessCommand> context)
    {
        Console.WriteLine($"Starting process for ID {context.Message.Id}...");
        await Task.Delay(5000);
        Console.WriteLine("Process finished!");

        await context.RespondAsync(new LongRunningCommandResponse
        {
            Id = context.Message.Id
        });
    }
}