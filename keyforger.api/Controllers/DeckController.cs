using System.ComponentModel.DataAnnotations;

using keyforger.application;
using keyforger.application.DTO;
using keyforger.domain;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace keyforger.api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class DeckController : ControllerBase
{
    private static readonly string[] SampleDeckNames = new[]
    {
        "Seba, Eater of Pizzas / Sample",
        "S. Warrington, Supervale's Ugly Warlord / Sample",
        "Aurora, Peculiar Citadel Exorcist / Sample"
    };

    private readonly IDeckRepository _deckRepository;
    private readonly ILogger<DeckController> _logger;
    private readonly IMediator _mediatR;

    public DeckController(IDeckRepository deckRepository, ILogger<DeckController> logger, IMediator mediatR)
    {
        _deckRepository = deckRepository;
        _logger = logger;
        _mediatR = mediatR;
    }

    [HttpGet]
    public async Task<IEnumerable<DeckViewModel>> GetDecksAsync()
    {
      var decks = await _mediatR.Send(new GetAllDecksQuery());

      return decks;
    }
    
    [HttpGet]
    [ActionName("GetDeck")]
    [Route("{deckId:guid}")]
    public async Task<Deck> GetDeck([FromRoute][Required] Guid deckId)
    {
        var deck = await _mediatR.Send(new GetDeckQuery(deckId));

        return deck;
    }

    [HttpPost]
    public async Task<ActionResult> AddDeck([FromBody] AddDeckCommand deckBody)
    {
        // send this command to mediator handle method
        // the mediatr send method will hanbdle it
        // inject it into this!

        // return HTTP 201 with deckId as Guid

        // send is for sending IRequests (from MediatR)
        // publish for notifcations (you can have multiple handlers) and no return value

        var deckId = await _mediatR.Send(deckBody);

        return CreatedAtAction(nameof(GetDeck), new { deckId = deckId }, deckId);
    }
}