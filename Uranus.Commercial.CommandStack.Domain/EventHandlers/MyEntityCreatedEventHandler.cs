using System.Threading.Tasks;
using Uranus.Commercial.CommandStack.Events;
using Uranus.Commercial.Desnormalizer.Projections.Common;
using Uranus.Commercial.Infrastructure.Framework;
using Uranus.Commercial.QueryStack.Model;

namespace Uranus.Commercial.CommandStack.Domain.EventHandlers
{
    public class MyEntityCreatedEventHandler : 
        IEventHandler<MyEntityCreatedEvent>
    {
        private readonly IProjectionWriter _elasticSearchProjectionWriter;

        public MyEntityCreatedEventHandler(IProjectionWriter elasticSearchProjectionWriter)
        {
            this._elasticSearchProjectionWriter = elasticSearchProjectionWriter;
        }

        public async Task HandleAsync(MyEntityCreatedEvent @event)
        {
            _elasticSearchProjectionWriter.Add<MyEntityView>(new MyEntityView()
            {
                Id = @event.AggregateId,
                PropertyOne = @event.PropertyOne,
                ProperyTwo = @event.PropertyTwo
            });

            await Task.CompletedTask;    
        }
    }
}