namespace keyforger.domain
{
  public class DeckCreatedEvent : IEvent
  {
    public Guid DeckId { get; set; }
    public List<House> Houses { get; set; } = new List<House>() {};
  }
}
