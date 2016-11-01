using System;
using System.Threading.Tasks;

namespace Uranus.Commercial.Infrastructure.Framework
{
    public interface IEventBus
    {
        Task SendAsync<TEvent>(TEvent @event) where TEvent : class, IEvent;

        Task RegisterHandler(Type eventType, Type eventHandler);
    }
}
