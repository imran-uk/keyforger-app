namespace keyforger.application.DTO
{
  public class DeckViewModel
  {
    public Guid DeckId { get; }
    public string Name { get; private set; }

    private DeckViewModel()
    {
      
    }

    public DeckViewModel(Guid deckId, string name)
    {
      this.DeckId = deckId;
      this.Name = name;
    }
  }
}
