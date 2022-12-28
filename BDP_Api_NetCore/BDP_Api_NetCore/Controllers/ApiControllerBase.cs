using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;

namespace BDP_Api_NetCore.Controllers
{
    [Route("api/[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        protected IMediator Mediator => HttpContext.RequestServices.GetService<IMediator>();
       
        protected IMapper Mapper => HttpContext.RequestServices.GetService<IMapper>();

        protected ILogger Logger => HttpContext.RequestServices.GetService<ILogger>() ?? NullLogger.Instance;
    }
}
