namespace Uranus.Commercial.Infrastructure.Framework
{
    public interface IApplyEvent<in TEvent> where TEvent : class, IEvent
    {
        void Apply(TEvent @event);
    }
}
