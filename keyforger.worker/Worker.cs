using keyforger.domain;

namespace keyforger.worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IEventConsumer _eventConsumer;

    // TODO
    //
    // do not directly inject ICOnnFactory
    //
    // this class might change, eg. to SQS
    //
    // think if queueing clas smight change

    public Worker(IEventConsumer eventConsumer, ILogger<Worker> logger)
    {
      _eventConsumer = eventConsumer;
      _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        // TODO
        // think of how to get messages to dead letter queue
        // eg. send invalid message that cannot be deserialized
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

        await Task.Delay(10000, stoppingToken);
      }
    }
}
