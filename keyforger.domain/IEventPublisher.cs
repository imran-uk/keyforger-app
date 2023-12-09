namespace keyforger.domain
{
  public interface IEventPublisher
  {
    public Task PublishEvent(IEvent @event);
  }
}