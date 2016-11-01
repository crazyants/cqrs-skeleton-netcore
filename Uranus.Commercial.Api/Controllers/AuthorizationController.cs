using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Server;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using OpenIddict;
using System.Security.Claims;
using System.Threading.Tasks;
using Uranus.Commercial.Security.Model;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Uranus.Commercial.Api.Controllers
{
    [AllowAnonymous]
    public class AuthorizationController : Controller
    {
        private OpenIddictUserManager<ApplicationUser> _userManager;

        public AuthorizationController(OpenIddictUserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }

        [HttpPost("~/connect/token")]
        [Produces("application/json")]
        public async Task<IActionResult> Exchange()
        {
            var request = HttpContext.GetOpenIdConnectRequest();

            if(request.IsPasswordGrantType())
            {
                var user = await this._userManager.FindByNameAsync(request.Username);
                if(user == null)
                {
                    return BadRequest(new OpenIdConnectResponse
                    {
                         Error = OpenIdConnectConstants.Errors.InvalidGrant,
                         ErrorDescription = "The username is invalid."
                    });
                }

                if(!await this._userManager.CheckPasswordAsync(user, request.Password))
                {
                    if(this._userManager.SupportsUserLockout)
                    {
                        await this._userManager.AccessFailedAsync(user);
                    }

                    return BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The username/password is invalid."
                    });
                }

                if(this._userManager.SupportsUserLockout)
                {
                    await this._userManager.ResetAccessFailedCountAsync(user);
                }

                var identity = await this._userManager.CreateIdentityAsync(user, request.GetScopes());
                var ticket = (AuthenticationTicket)null;

                identity.AddClaim("user_name", user.UserName, OpenIdConnectConstants.Destinations.AccessToken, OpenIdConnectConstants.Destinations.IdentityToken);
                ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), new AuthenticationProperties(), OpenIdConnectServerDefaults.AuthenticationScheme);
                ticket.SetResources(request.GetResources());
                ticket.SetScopes(request.GetScopes());

                return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
            }

            return BadRequest(new OpenIdConnectResponse
            {
                Error = OpenIdConnectConstants.Errors.UnsupportedGrantType,
                ErrorDescription = "The specified grant_type is not supported."
            });
        }
    }
}
