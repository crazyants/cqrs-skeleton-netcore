using System.Threading.Tasks;

namespace Uranus.Commercial.Infrastructure.Framework
{
    public interface IEventHandler<in TEvent> where TEvent : class, 
        IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}
