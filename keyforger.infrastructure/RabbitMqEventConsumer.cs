using System.Formats.Asn1;
using System.Text;
using System.Threading.Channels;

using keyforger.domain;

using Microsoft.Extensions.Options;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace keyforger.infrastructure;

public class RabbitMqEventConsumer : IEventConsumer
{
  private readonly ILogger<RabbitMqEventConsumer> _logger;
  private readonly RabbitMqOptions _rabbitMqOptions;
  private readonly IConnection _connection;
  private readonly IServiceProvider _serviceProvider;
  private IModel _channel;
  private EventingBasicConsumer _consumer;


  public RabbitMqEventConsumer(IConnectionFactory rabbitConnectionFactory, IOptions<RabbitMqOptions> rabbitMqOptions, ILogger<RabbitMqEventConsumer> logger, IServiceProvider serviceProvider)
  {
    _rabbitMqOptions = rabbitMqOptions.Value;

    rabbitConnectionFactory.UserName = _rabbitMqOptions.User;
    rabbitConnectionFactory.Password = _rabbitMqOptions.Password;

    _connection = rabbitConnectionFactory.CreateConnection(_rabbitMqOptions.Hostname);
    _logger = logger;
    _serviceProvider = serviceProvider;

    SetUp_Consumer();
  }

  private void SetUp_Consumer()
  {
    _channel = _connection.CreateModel();

    _consumer = new EventingBasicConsumer(_channel);

    _consumer.Received += (_, eventArgs) =>
    {
      // no idea what this does
      using var scope = _serviceProvider.CreateScope();
      var logger = scope.ServiceProvider.GetService<ILogger<RabbitMqEventConsumer>>();

      var body = eventArgs.Body.ToArray();
      var message = Encoding.UTF8.GetString(body);

      logger.LogInformation($"{nameof(RabbitMqEventConsumer)} has rx a message: {message} / {eventArgs.BasicProperties.CorrelationId}");

      // process event

      // send ack - if success
      // send nack - if error

    };

    // see "Fair dispatch" here: https://www.rabbitmq.com/tutorials/tutorial-two-dotnet.html

    _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

    _channel.BasicConsume(queue: _rabbitMqOptions.Queue, autoAck: true, consumer: _consumer);
  }
}