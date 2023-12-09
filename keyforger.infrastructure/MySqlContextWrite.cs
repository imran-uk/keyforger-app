using keyforger.domain;

using Microsoft.EntityFrameworkCore;

namespace keyforger.infrastructure;

public class MySqlContextWrite : DbContext
{
    public MySqlContextWrite(DbContextOptions<MySqlContextWrite> optionsBuilderOptions) : base(optionsBuilderOptions)
    {
    }

    // this class contains the entities we will save in mysql-database
    // moar infos: https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-6.0

    public DbSet<Deck> Deck { get; set; }

    public DbSet<Card> Cards { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // define primary keys
        modelBuilder.Entity<Deck>().HasKey(d => d.DeckId);
        modelBuilder.Entity<Card>().HasKey(c => c.CardId);

        // define relations
        modelBuilder.Entity<Deck>().HasMany(d => d.Cards);

        // lets use the enum convertor we created
        modelBuilder.Entity<Deck>().
          Property(d => d.Houses).HasConversion<EnumCollectionConvertor<House>>();
        // now same for CardPips oin Card
        modelBuilder.Entity<Card>().
          Property(c => c.CardPips).HasConversion<EnumCollectionConvertor<CardPips>>();
    }

    // public DbSet<House> House { get; set; }
}