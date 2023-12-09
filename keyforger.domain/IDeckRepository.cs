namespace keyforger.domain;

// TODO
// next step is to imnplement this in MySQL context
public interface IDeckRepository
{
    // get deck
    public Task<Deck> getDeckAsync(Guid deckId);

    // get ALL decks
    public Task<IEnumerable<Deck>> getAllAsync();

    // store deck
    // returns void on success or exception when error
    public Task storeDeckAsync(Deck deck);

    // update deck

    // delete deck
}