using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Uranus.Commercial.Security.Model;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Uranus.Commercial.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }

        [HttpPost]
        [AllowAnonymous]        
        public async Task<IdentityResult> Post()
        {
            var applicationUser = new ApplicationUser
            {
                UserName = "test",
                Email = "test@test.com"
            };

            return await this._userManager.CreateAsync(applicationUser, "test1234");
        }
    }
}
