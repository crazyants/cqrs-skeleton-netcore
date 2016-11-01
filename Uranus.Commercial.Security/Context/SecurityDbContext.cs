using Microsoft.EntityFrameworkCore;
using OpenIddict;
using Uranus.Commercial.Security.Model;

namespace Uranus.Commercial.Security.Context
{
    public class SecurityDbContext : OpenIddictDbContext<ApplicationUser, ApplicationRole>
    {
        public SecurityDbContext(DbContextOptions<SecurityDbContext> options) 
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
