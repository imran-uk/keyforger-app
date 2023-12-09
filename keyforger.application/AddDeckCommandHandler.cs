using keyforger.domain;

namespace keyforger.application;

using System.Threading;
using System.Threading.Tasks;

using MediatR;

public class AddDeckCommandHandler : IRequestHandler<AddDeckCommand, Guid>
{
  private readonly IDeckRepository _deckRepository;
  private readonly IEventPublisher _eventPublisher;

  public AddDeckCommandHandler(IDeckRepository deckRepository, IEventPublisher eventPublisher)
  {
    _deckRepository = deckRepository;
    _eventPublisher = eventPublisher;
  }

  public async Task<Guid> Handle(AddDeckCommand request, CancellationToken cancellationToken)
  {
    // generate deck
    var cardList = Deck.CreateCardListForKeyForgeDeck(request.HouseList);
    var deck = new Deck(Guid.NewGuid(), request.DeckName, request.SetName, request.HouseList, cardList);

    // imran
    // generate the event here ?
    var @event = new DeckCreatedEvent { DeckId = deck.DeckId, Houses = deck.Houses };

    // thios should throw an exception if the deck cannot be stored to database
    await _deckRepository.storeDeckAsync(deck);

    // this should happen AFTER we store the deck, that way we can have
    // confidence that a deck was actually created.
    //
    // we would also need some way to persist these events in case they cannot be published.
    // they would then need some means to be processed again, maybe another worker...
    await _eventPublisher.PublishEvent(@event);

    return deck.DeckId;
  }
}