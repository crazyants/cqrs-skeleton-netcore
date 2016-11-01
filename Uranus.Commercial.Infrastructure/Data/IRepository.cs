using Uranus.Commercial.Infrastructure.Framework;

namespace Uranus.Commercial.Infrastructure.Data
{
    public interface IRepository<TAggregate> where TAggregate : class, IAggregateRoot
    {
        void Save(TAggregate aggregate);

        void Delete(TAggregate aggregate);
    }
}
