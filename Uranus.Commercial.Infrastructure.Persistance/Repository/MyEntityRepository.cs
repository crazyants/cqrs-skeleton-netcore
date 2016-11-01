using Microsoft.EntityFrameworkCore;
using Uranus.Commercial.CommandStack.Domain.Model;
using Uranus.Commercial.CommandStack.Domain.Repository;
using Uranus.Commercial.Infrastructure.Data;
using Uranus.Commercial.Infrastructure.Persistance.Context;

namespace Uranus.Commercial.Infrastructure.Persistance.Repository
{
    public class MyEntityRepository : BaseRepository<MyEntity>, IMyEntityRepository
    {
        public MyEntityRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        { }
    }
}
