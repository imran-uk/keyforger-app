using keyforger.domain;

namespace keyforger.infrastructure
{
  public class DeckRepositoryWithLogging : IDeckRepository
  {
    private readonly ILogger _logger;
    private readonly IDeckRepository _deckRepository;

    public DeckRepositoryWithLogging(IDeckRepository deckRepository, ILogger logger)
    {
      _logger = logger;
      _deckRepository = deckRepository;
    }

    public Task<Deck> getDeckAsync(Guid deckId)
    {
      _logger.LogInformation("getDeckAsync was called");

      var deck = _deckRepository.getDeckAsync(deckId);

      _logger.LogInformation("getDeckAsync call was finished");

      return deck;
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
