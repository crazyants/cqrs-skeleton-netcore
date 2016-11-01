using System.Threading.Tasks;
using Uranus.Commercial.CommandStack.Events;
using Uranus.Commercial.Infrastructure.Framework;

namespace Uranus.Commercial.CommandStack.Domain.EventHandlers
{
    public class MyEntityCreated2EventHandler : 
        IEventHandler<MyEntityCreatedEvent>
    {
        public async Task HandleAsync(MyEntityCreatedEvent @event)
        {
            await Task.CompletedTask;
        }
    }
}