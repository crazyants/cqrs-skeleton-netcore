using Microsoft.EntityFrameworkCore;
using Uranus.Commercial.CommandStack.Domain.Model;

namespace Uranus.Commercial.Infrastructure.Persistance.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<MyEntity> MyEntities { get; set; }
    }
}
