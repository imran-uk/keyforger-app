using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace keyforger.infrastructure;

public class RabbitMqOptions
{
  public const string RabbitMQ = "RabbitMQ";

  public string User { get; set; } = String.Empty;
  public string Password { get; set; } = String.Empty;
  public string Queue { get; set; } = String.Empty;
  public string Hostname { get; set; } = String.Empty;
}