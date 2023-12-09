using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using keyforger.domain;
using keyforger.infrastructure;

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

using Moq;

namespace keyforger.UnitTests
{
  internal class DeckRepositoryDecoratorTests
  {
    // unit test method
    [Test]
    public void Test1()
    {
      Mock<ILogger> _loggerMock = new Mock<ILogger>();
      var deckRepo = new DeckRepositoryForTest();
      
      var deckRepoWithCache = new DeckRepositoryWithCache(deckRepo);
      var deckRepoWithLogging = new DeckRepositoryWithLogging(deckRepo, _loggerMock.Object);

      // any differtence ?
      var deckRepositoryWithCacheAndLogging = new DeckRepositoryWithLogging(deckRepoWithCache, _loggerMock.Object);

      var deckRepositoryWithLoggingAndCache = new DeckRepositoryWithCache(deckRepoWithLogging);

    }

  }

  internal class DeckRepositoryForTest : IDeckRepository
  {
    public Task<Deck> getDeckAsync(Guid deckId)
    {
      return Task.FromResult(new Deck(deckId,
        "Foo",
        KeyForgeSetName.AgeOfAscension,
        new List<House>
        {
          House.Brobnar,
          House.Dis,
          House.Mars
        },
        new List<Card>()));

      throw new NotImplementedException();
    }

    public Task<IEnumerable<Deck>> getAllAsync()
    {
      throw new NotImplementedException();
    }

    public Task storeDeckAsync(Deck deck)
    {
      throw new NotImplementedException();
    }
  }

}
