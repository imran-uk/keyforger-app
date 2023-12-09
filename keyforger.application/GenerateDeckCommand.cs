using keyforger.domain;

namespace keyforger.application;


// TODO
// think about putting all the commands into a Commands dir
// (then rename them to not have Command)
public class GenerateDeckCommand
{
    // the House limit of 3 is a detail that should be in domain layer...
    public IEnumerable<House> HouseList { get; set; }
    public string DeckName { get; set; }

    // maybe a requestdate can be added later
    public GenerateDeckCommand(IEnumerable<House> houseList, string deckName)
    {
        HouseList = houseList;
        DeckName = deckName;
    }
}