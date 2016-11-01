using Microsoft.EntityFrameworkCore;
using Uranus.Commercial.Infrastructure.Framework;

namespace Uranus.Commercial.Infrastructure.Data
{
    public class BaseRepository<TAggregate> : IRepository<TAggregate> 
        where TAggregate: class, IAggregateRoot
    {
        protected DbContext _dbContext;
        protected DbSet<TAggregate> _dbSetAggregate;

        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSetAggregate = _dbContext.Set<TAggregate>();
        }

        public void Delete(TAggregate aggregate)
        {
            _dbContext.Entry(aggregate).State = EntityState.Deleted;
        }

        public void Save(TAggregate aggregate)
        {
            _dbContext.Entry(aggregate).State = EntityState.Added;
        }
    }
}
