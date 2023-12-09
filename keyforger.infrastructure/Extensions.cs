using keyforger.domain;

using Microsoft.EntityFrameworkCore;

using RabbitMQ.Client;

namespace keyforger.infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
      ConfigurationManager builderConfiguration)
    {
        // TODO
        // how to use values from config here?
        var connectionString = "server=localhost; database=keyforger; user=keyforger; password=foobar";
        
        services.AddDbContext<MySqlContextWrite>(
            ctx => ctx.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)), 
            ServiceLifetime.Transient);
        
        services.AddDbContext<MySqlContextRead>(
          ctx => ctx.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)), 
          ServiceLifetime.Transient);
        
        services.AddScoped<IDeckRepository, DeckRepository>();
        services.AddScoped<IEventPublisher, RabbitMqEventPublisher>();
        services.AddScoped<ILogger, Logger<RabbitMqEventPublisher>>();
        services.AddScoped<ILogger, Logger<MySqlContextDesignTimeFactory>>();
        services.AddScoped<IConnectionFactory, ConnectionFactory>();

        // explanation of Options pattern
        // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-7.0
        services.Configure<RabbitMqOptions>(builderConfiguration.GetSection(RabbitMqOptions.RabbitMQ));
        services.Configure<MySqlOptions>(builderConfiguration.GetSection(MySqlOptions.MySql));

        return services;
    }
}