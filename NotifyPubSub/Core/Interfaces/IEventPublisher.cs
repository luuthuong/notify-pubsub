namespace VNG.SocialNotify.Core.Interfaces;

public interface IEventPublisher
{
    void Publish<TEvent>(TEvent @event) where TEvent : IEvent;
}
