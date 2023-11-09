using BackgroundServiceExamples.Core;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .UseMassTransit((context, configurator) =>
    {
        configurator.UsingRabbitMq((rabbitcontext, rabbitMqConfigurator) =>
        {
            rabbitMqConfigurator.Host("localhost", "/", host =>
            {
                host.Username("user");
                host.Password("password");
            });
        });
    })
    .Build();

await host.StartAsync();

while (true)
{
    Console.WriteLine("Press any key to start a long running process or ESC to exit");
    var key = Console.ReadKey();
    if (key.Key == ConsoleKey.Escape)
    {
        await host.StopAsync();
        break;
    }
    else
    {
        var bus = host.Services.GetRequiredService<IBus>();
        var id = Guid.NewGuid();
        
        Console.WriteLine($"Publishing long running job for ID {id}.");
        await bus.Publish(new StartLongRunningProcessCommand
        {
            Id = id
        });
    }
}