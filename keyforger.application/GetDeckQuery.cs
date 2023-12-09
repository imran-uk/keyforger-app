using keyforger.domain;

using MediatR;

namespace keyforger.application
{
  public class GetDeckQuery : IRequest<Deck>
  {
    public Guid DeckId { get; set; }

    public GetDeckQuery(Guid deckId)
    {
      DeckId = deckId;
    }
  }
}
