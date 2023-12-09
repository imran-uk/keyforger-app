using keyforger.domain;

using MediatR;

namespace keyforger.application;

public class GetDeckQueryHandler : IRequestHandler<GetDeckQuery, Deck>
{
  private readonly IDeckRepository _deckRepository;

  public GetDeckQueryHandler(IDeckRepository deckRepository)
  {
    _deckRepository = deckRepository;
  }

  public async Task<Deck> Handle(GetDeckQuery request, CancellationToken cancellationToken)
  {
    return await _deckRepository.getDeckAsync(request.DeckId);
  }
}