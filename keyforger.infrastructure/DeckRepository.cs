using keyforger.domain;

using Microsoft.EntityFrameworkCore;

namespace keyforger.infrastructure;

class DeckRepository : IDeckRepository
{

    // TODO
    // add logging before & after each method call...
    // how?
    // ... decorator pattern !
    // see DeckRepositoryWithLogging

    private readonly MySqlContextWrite _mysqlContext;

    public DeckRepository(MySqlContextWrite mysqlContext)
    {
        _mysqlContext = mysqlContext;
    }

    public async Task<Deck> getDeckAsync(Guid deckId)
    {
        // logger got deck id this

        var deck = await _mysqlContext.Deck.Include(d => d.Cards)
            .SingleOrDefaultAsync(d => d.DeckId == deckId);

        if (deck == null)
        {
            throw new Exception("deck not known");
        }

        // logger found deck

        return deck;
    }

    public async Task<IEnumerable<Deck>> getAllAsync()
    {
        var decks = await _mysqlContext.Deck.ToArrayAsync();

        return decks;
    }

    public async Task storeDeckAsync(Deck deck)
    {
        await _mysqlContext.Deck.AddAsync(deck);
        await _mysqlContext.SaveChangesAsync();
    }
}