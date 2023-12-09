using keyforger.domain;

namespace keyforger.infrastructure
{
  public class DeckRepositoryWithCache : IDeckRepository
  {
    private Dictionary<Guid, Deck> _deckCache;
    private readonly IDeckRepository _deckRepository;

    public DeckRepositoryWithCache(IDeckRepository deckRepository)
    {
      _deckRepository = deckRepository;
      _deckCache = new Dictionary<Guid, Deck>();
    }

    public async Task<Deck> getDeckAsync(Guid deckId)
    {
      // if deck is in cache
      //  return deck from cache
      // else
      //  getDeck from database
      //  store deck in cache
      //  return the deck

      if (_deckCache.ContainsKey(deckId))
      {
        return _deckCache[deckId];
      }

      var deck = await _deckRepository.getDeckAsync(deckId);
      _deckCache.Add(deckId, deck);

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
