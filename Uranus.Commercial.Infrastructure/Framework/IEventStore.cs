using System;
using System.Threading.Tasks;

namespace Uranus.Commercial.Infrastructure.Framework
{
    public interface IEventStore
    {
        Task PersistAsync<TAggregate>(TAggregate aggregate) where TAggregate : class, IAggregateRoot;

        Task<TAggregate> GetByIdAsync<TAggregate>(Guid id) where TAggregate : IAggregateRoot, new();
    }
}
