using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace keyforger.infrastructure
{
  public class MySqlContextDesignTimeFactory : IDesignTimeDbContextFactory<MySqlContextWrite>
  {
    private readonly ILogger<MySqlContextDesignTimeFactory> _logger;
    private readonly MySqlOptions _mySqlOptions;

    public MySqlContextDesignTimeFactory(IOptions<MySqlOptions> mySqlOptions, ILogger<MySqlContextDesignTimeFactory> logger)
    {
      _mySqlOptions = mySqlOptions.Value;
      _logger = logger;
    }

    public MySqlContextWrite CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<MySqlContextWrite>();
      var connectionString = $"server={_mySqlOptions.Server}; database={_mySqlOptions.Database}; user={_mySqlOptions.User}; password={_mySqlOptions.Password}";
      optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

      _logger.LogInformation($"Connection made from {nameof(MySqlContextDesignTimeFactory)}");

      return new MySqlContextWrite(optionsBuilder.Options);
    }
  }
}
