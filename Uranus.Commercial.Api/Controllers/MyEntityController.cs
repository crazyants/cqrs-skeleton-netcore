using Microsoft.AspNetCore.Mvc;
using Uranus.Commercial.Api.ViewModels;
using Uranus.Commercial.CommandStack.Commands;
using Uranus.Commercial.Infrastructure.Framework;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Uranus.Commercial.Api.Controllers
{
    [Route("api/[controller]")]
    public class MyEntityController : Controller
    {
        private readonly ICommandBus _bus;

        public MyEntityController(ICommandBus bus)
        {
            this._bus = bus;
        }

        [HttpPost]
        public void Post([FromBody] MyEntityViewModel vm)
        {
            _bus.SendAsync(new CreateMyEntityCommand(vm.PropertyOne, vm.PropertyTwo));
        }
    }
}
