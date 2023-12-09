using keyforger.application.DTO;

using MediatR;

namespace keyforger.application;

public class GetAllDecksQuery : IRequest<IEnumerable<DeckViewModel>>
{
  // TODO
  // might want to have filter or sort params here eventually...

  public GetAllDecksQuery()
  {
      
  }
}