namespace keyforger.domain;

public class DeckGenerator
{
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