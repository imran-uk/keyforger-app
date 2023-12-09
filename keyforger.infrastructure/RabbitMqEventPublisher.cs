using keyforger.domain;
using System.Text;
using System.Text.Json;

using RabbitMQ.Client;
using Microsoft.Extensions.Options;

namespace keyforger.infrastructure;

public class RabbitMqEventPublisher : IEventPublisher
{
  private readonly ILogger<RabbitMqEventPublisher> _logger;
  private readonly IConnectionFactory _rabbitConnectionFactory;
  private readonly RabbitMqOptions _rabbitMqOptions;

  private IModel _channel;

  public RabbitMqEventPublisher(IConnectionFactory rabbitConnectionFactory, IOptions<RabbitMqOptions> rabbitMqOptions, ILogger<RabbitMqEventPublisher> logger)
  {
    _rabbitMqOptions = rabbitMqOptions.Value;
    _rabbitConnectionFactory = rabbitConnectionFactory;

    _rabbitConnectionFactory.UserName = _rabbitMqOptions.User;
    _rabbitConnectionFactory.Password = _rabbitMqOptions.Password;
    
    _logger = logger;

    _channel = _rabbitConnectionFactory.CreateConnection(_rabbitMqOptions.Hostname).CreateModel();
  }

  public Task PublishEvent(IEvent @event)
  {
    // using var connection = _rabbitConnectionFactory.CreateConnection(_rabbitMqOptions.Hostname);
    //using var channel = connection.CreateModel();

    // TODO
    // figure out how to set these,
    // in particular the correlationId (see the interface)
    IBasicProperties basicProperties = _channel.CreateBasicProperties();
    basicProperties.CorrelationId = Guid.NewGuid().ToString();
    basicProperties.Persistent = true;

    var body = JsonSerializer.Serialize<object>(@event);

    // if exchange is empty then just give queue name
    _channel.BasicPublish(exchange: string.Empty,
      routingKey: _rabbitMqOptions.Queue,
      basicProperties: basicProperties,
      body: Encoding.UTF8.GetBytes(body));

    _logger.LogInformation($"{nameof(RabbitMqEventPublisher)} has sent a message: {body} / id: {basicProperties.CorrelationId}");

    return Task.CompletedTask;
  }
}