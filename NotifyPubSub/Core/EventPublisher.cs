
namespace VNG.SocialNotify.Core;

public class EventPublisher(Func<Type, object> func) : IEventPublisher
{
    public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
    {
        (func(typeof(IEventSubcriber<TEvent>)) as IEventSubcriber<TEvent>)?.Subscribe(@event);
    }
}
