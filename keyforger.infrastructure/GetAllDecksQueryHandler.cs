using keyforger.application;
using keyforger.application.DTO;

using MediatR;

namespace keyforger.infrastructure;

public class GetAllDecksQueryHandler : IRequestHandler<GetAllDecksQuery, IEnumerable<DeckViewModel>>
{
  private readonly MySqlContextRead _context;

  public GetAllDecksQueryHandler(MySqlContextRead context)
  {
    _context = context;
  }

  public Task<IEnumerable<DeckViewModel>> Handle(GetAllDecksQuery request, CancellationToken cancellationToken)
  {
    // query the database and return results using the DeckViewModel
    return Task.FromResult(_context.Deck.AsEnumerable());
  }
}