namespace keyforger.infrastructure
{
  public class MySqlOptions
  {
    public const string MySql = "MySQL";

    public string User { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public string Database { get; set; } = String.Empty;
    public string Server { get; set; } = String.Empty;
  }
}