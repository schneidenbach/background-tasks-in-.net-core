using BackgroundServiceExamples.MessageBasedWorker;
using MassTransit;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .UseMassTransit((context, configurator) =>
    {
        configurator.UsingRabbitMq((rabbitcontext, rabbitMqConfigurator) =>
        {
            //host from localhost
            rabbitMqConfigurator.Host("localhost", "/", host =>
            {
                host.Username("user");
                host.Password("password");
            });
            
            rabbitMqConfigurator.ReceiveEndpoint("long-running-process", e =>
            {
                e.Consumer<StartLongRunningProcessCommandConsumer>();
            });
        });
    })
    .Build();

host.Run();