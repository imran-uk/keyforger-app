using FluentAssertions;

using keyforger.domain;

// ReSharper disable once IdentifierTypo
namespace keyforger.UnitTests;

public class DeckTests
{
    [SetUp]
    public void Setup()
    {
    }

    // TODO
    // look into...
    // new C# 10 nullable string ?
    // see NRT

    private List<Card> generateCardList(int numberOfCards)
    {
        var cardList = new List<Card>();

        // TODO randomize the Card pips (random selection of 0-4)
        // but really there should be a proper card list generator :D

        cardList = Enumerable.Range(0, numberOfCards).Select(_ => new Card("Placeholder Card Name",
            "Placeholder Card Description",
            GetRandomHouse(),
            GetRandomCardType(),
            new List<CardPips>
            {
                CardPips.Aember,
                CardPips.Aember
            })).ToList();

        return cardList;
    }

    private static House GetRandomHouse()
    {
        var houseValues = Enum.GetValues<House>();
        var houseCount = houseValues.Length;

        return houseValues[Random.Shared.Next(houseCount)];
    }

    private CardType GetRandomCardType()
    {
        var cardTypeValues = Enum.GetValues<CardType>();

        return cardTypeValues[Random.Shared.Next(cardTypeValues.Length)];
    }

    private static List<Card> GenerateCardListWithHouse(int numberOfCards, House house)
    {
        var cardList = new List<Card>();

        // TODO randomize the card house each time...
        // along with CardType and Aember Pips

        cardList = Enumerable.Range(0, numberOfCards).Select(_ => new Card("Placeholder Card Name",
            "Placeholder Card Description",
            house,
            CardType.Creature,
            new List<CardPips> { CardPips.Aember, CardPips.Aember })).ToList();

        return cardList;
    }

    private static Deck GenerateDeck(string deckName, List<Card> cardList)
    {
        var keyForgeSet = KeyForgeSetName.CallOfTheArchons;

        // TODO randomize house list if none input?
        var houseList = new List<House>
        {
            House.Mars,
            House.Brobnar,
            House.Shadows
        };

        return new Deck(Guid.NewGuid(), deckName, keyForgeSet, houseList, cardList);
    }

    private Deck GenerateDeckWithHouses(string deckName, List<Card> cardList, List<House> houseList)
    {
        // TODO choose set at random
        var keyForgeSet = KeyForgeSetName.CallOfTheArchons;

        return new Deck(Guid.NewGuid(), deckName, keyForgeSet, houseList, cardList);
    }

    [Test]
    public void deck_must_have_name()
    {
        // Arrange
        var cardList = generateCardList(36);

        // Act & Assert
        Action act = () => GenerateDeck(String.Empty, cardList);
        act.Should().Throw<Exception>("Deck name cannot be Null or Empty");
    }

    [Test]
    public void deck_must_have_three_houses()
    {
        // Arrange
        var deckId = Guid.NewGuid();
        var deckName = "Foobar, Eater of Pirogis";
        var keyForgeSet = KeyForgeSetName.CallOfTheArchons;
        var houseList = new List<House>
        {
            House.Mars,
            House.Brobnar,
            House.Logos,
            House.Dis
        };

        var cards = generateCardList(36);

        // Act & Assert
        Action act = () =>
        {
            Deck deck = new Deck(deckId, deckName, keyForgeSet, houseList, cards);
        };
        act.Should().Throw<Exception>().WithMessage("A deck needs three houses");
    }

    [Test]
    public void deck_must_have_36_cards()
    {
        // Arrange
        var cardList = generateCardList(40);
        
        // Act & Assert
        Action act = () => GenerateDeck("Doobrey, Whatsit of the Thingymajig", cardList);
        act.Should().Throw<Exception>().WithMessage("A deck needs to be 36 cards");
    }

    [Test]
    public void all_cards_in_deck_must_belong_to_one_of_three_houses_assigned_to_deck()
    {
        // Arrange
        var cardList = GenerateCardListWithHouse(36, House.StarAlliance);
        
        // Act & Assert
        Action act = () => GenerateDeckWithHouses("Tomas, Ruler of the Eagles Reaches", 
            cardList, new List<House> { House.Dis, House.Logos, House.Saurian });
        
        act.Should().Throw<Exception>()
            .WithMessage("Deck has a card that does not belong to one of the three houses");

        // TODO can save this for the positive test
        // Act & Assert
        // foreach (var card in deck.Cards)
        // {
        // card.House.Should().BeOneOf(houseList);
        // }
    }
}