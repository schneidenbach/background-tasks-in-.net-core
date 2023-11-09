using BackgroundServiceExamples.Core;
using MassTransit;

namespace BackgroundServiceExamples.MessageBasedWorker;

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