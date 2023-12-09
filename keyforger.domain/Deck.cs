using System.ComponentModel;

namespace keyforger.domain;

public class Deck
{
    private Deck()
    {
        
    }

    public Guid DeckId { get; private set; }
    public string Name { get; private set; }
    public KeyForgeSetName Set { get; private set; }

    public List<House> Houses { get; private set; }
    public IEnumerable<Card> Cards { get; private set; }

    // TODO
    // lets TDD the deck ctor
    public Deck(Guid deckId, string name, KeyForgeSetName set, List<House> houses, List<Card> cards)
    {
        DeckId = deckId;
        Name = string.IsNullOrEmpty(name) ? throw new Exception("Deck name cannot be Null or Empty") : name;
        Set = set;
        Houses = ThrowIfNotValidNumberOfHousesPresent(houses);

        ValidateCards(cards);

        Cards = cards;
    }

    private void ValidateCards(List<Card> cards)
    {
        if (cards.Count != 36)
        {
            throw new Exception("A deck needs to be 36 cards");
        }

        if (cards.Any(x => !Houses.Contains(x.House)))
        {
            throw new Exception("Deck has a card that does not belong to one of the three houses");
        }
    }

    // method to check if exactly three houses present in house list
    public static List<House> ThrowIfNotValidNumberOfHousesPresent(List<House> houseList)
    {
        if (houseList.Count != 3)
        {
            throw new Exception("A deck needs three houses");
        }

        return houseList;
    }

    // TODO
    // move to generaotr class
    public static List<Card> CreateCardListForKeyForgeDeck(List<House> houses)
    {
        var cardList = new List<Card>();

        foreach (var house in houses)
        {
            for (int i = 0; i < 12; i++) {
                // TODO - need to add Card with all fields eventually
                // 
                // TODO look into the different ways of instantiating objects in C# ...
                cardList.Add(
                    new("Placeholder Card Name",
                        "Placeholder Card Description",
                        house,
                        CardType.Creature,
                        new List<CardPips>
                        {
                            CardPips.Aember,
                            CardPips.Aember
                        })
                    );
            }
        }
        
        return cardList;
    }
}

public class KeyForgeSet
{
    public KeyForgeSetName Name { get; }
    public DateTime ReleaseDate { get; }

    public KeyForgeSet(KeyForgeSetName name, DateTime releaseDate)
    {
        Name = name;
        ReleaseDate = releaseDate;
    }
}

public class Card
{
    // required for EF
    private Card()
    {
        
    }

    public Card(string name, string description, House house, CardType type, List<CardPips> cardPips)
    {
        Name = name;
        Description = description;
        House = house;
        Type = type;
        CardPips = cardPips;
    }

    public int CardId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public House House { get; private set; }
    public CardType Type { get; private set; }
    public List<CardPips> CardPips { get; private set; }
}

public enum CardPips
{
    Damage,
    Aember,
    Capture,
    Draw
}

public enum CardType
{
    Action,
    Creature,
    Upgrade,
    Artifact
}

public enum KeyForgeSetName
{
    [Description("Call of the Archons, the first set, aka Set1, aka CotA")]
    CallOfTheArchons,
    [Description("Age of Ascension, aka Set2, aka AoA")]
    AgeOfAscension,
    [Description("Worlds Collide")]
    WorldsCollide,
    [Description("Mass Mutation")]
    MassMutation,
    [Description("Dark Tidings")]
    DarkTidings,
    [Description("Winds of Exchange")]
    WindsOfExchange
}