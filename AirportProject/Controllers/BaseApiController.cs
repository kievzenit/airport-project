using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirportProject.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private ISender mediator = null!;
        protected ISender Mediator =>
            this.mediator ??= HttpContext.RequestServices.GetService(typeof(ISender)) as ISender;
    }
}
