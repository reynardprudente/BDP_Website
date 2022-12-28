using AutoMapper;
using BDP.Application.Command.Request;
using BDP.Application.Command.Request.Login;
using BDP.Application.Dto;
using BDP.Application.Enum;
using BDP_Api_NetCore.Model.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BDP_Api_NetCore.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel parameter, CancellationToken cancellation)
        {
            try
            { 
                var result = await Mediator.Send(new LoginCommandRequest()
                {
                    login = Mapper.Map<LoginDTO>(parameter)
                }, cancellation);

                if(result.Status == Status.Error)
                {
                    return Unauthorized(result.Message);
                }
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(result.Value),
                    expiration = result.Value.ValidTo
                });
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Critical, $"trace stack: {ex.Message}, {ex.InnerException}");
                return BadRequest(ErrorResource.General_Error);
            }
        }
    
    }
}
