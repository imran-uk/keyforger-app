using keyforger.domain;
using keyforger.infrastructure;
using keyforger.worker;

using RabbitMQ.Client;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<Worker>();

        services.AddSingleton<IConnectionFactory, ConnectionFactory>();
        services.AddSingleton<IEventConsumer, RabbitMqEventConsumer>();

        services.Configure<RabbitMqOptions>(context.Configuration.GetSection(RabbitMqOptions.RabbitMQ));
    })
    .Build();

host.Run();