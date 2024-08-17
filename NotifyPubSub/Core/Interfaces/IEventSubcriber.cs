namespace VNG.SocialNotify.Core.Interfaces;

public interface IEventSubcriber<TEvent> where TEvent : IEvent{
    void Subscribe(TEvent @event);
}
