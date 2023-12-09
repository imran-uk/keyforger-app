using keyforger.application.DTO;

using Microsoft.EntityFrameworkCore;

namespace keyforger.infrastructure;

public class MySqlContextRead : DbContext
{
    public MySqlContextRead(DbContextOptions<MySqlContextRead> optionsBuilderOptions) : base(optionsBuilderOptions)
    {
    }

    // this class contains the entities we will save in mysql-database
    // moar infos: https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-6.0

    public DbSet<DeckViewModel> Deck { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // define primary keys
        modelBuilder.Entity<DeckViewModel>().HasKey(d => d.DeckId);
    }
}