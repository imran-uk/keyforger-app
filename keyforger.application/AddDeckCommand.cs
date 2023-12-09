using keyforger.domain;

using MediatR;

namespace keyforger.application
{
    public class AddDeckCommand : IRequest<Guid>
    {
        //public AddDeckCommand() { }

        // what do we need to create a deck?
        // a guid
        // a deck name
        // house list
        // a set

        public Guid Id { get; set; }
        public string DeckName { get; set; }
        public List<House> HouseList { get; set; }
        public KeyForgeSetName SetName { get; set; }
    }

    // getdeckquery
}
