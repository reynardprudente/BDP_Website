using BDP.Application.Command.Request;
using BDP.Application.Command.Request.User;
using BDP.Application.Dto;
using BDP.Application.Enum;
using BDP.Application.Query.Request;
using BDP.Application.Query.Request.User;
using BDP_Api_NetCore.Model.User;
using BDP_Api_NetCore.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net.Mail;
using System.Reflection.Metadata;

namespace BDP_Api_NetCore.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellation)
        {
            try
            {
                var result = await Mediator.Send(
                 new GetUsersQueryRequest(), cancellation);
                var user = Mapper.Map<List<UserViewModel>>(result.Value);
                return Ok(user);
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Critical, $"trace stack: {ex.Message}, {ex.InnerException}");
                return BadRequest(ErrorResource.General_Error);
            }        
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserModel parameter, CancellationToken cancellation)
        {
            try
            {
                var user = Mapper.Map<UserDTO>(parameter);
                var result = await Mediator.Send(
                     new AddUserCommandRequest()
                     {
                         user = user,
                         ClaimsPrincipal = User
                     }, cancellation);
                if (result.Status == Status.Error)
                {
                    return BadRequest(result.Message);
                }
                return Ok(result.Message);
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Critical, $"trace stack: {ex.Message}, {ex.InnerException}");
                return BadRequest(ErrorResource.General_Error);           
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserModel parameter, CancellationToken cancellation)
        {
            try
            {
                var user = Mapper.Map<UserDTO>(parameter);
                var result = await Mediator.Send(
                   new UpdateUserCommandRequest()
                   {
                       user = user,
                       ClaimsPrincipal = User
                   }, cancellation);
                if (result.Status == Status.Error)
                {
                    return BadRequest(result.Message);
                }
                return Ok(result.Message);
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Critical, $"trace stack: {ex.Message}, {ex.InnerException}");
                return BadRequest(ErrorResource.General_Error);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string emailAddress, CancellationToken cancellation)
        {
            try
            {
                var result = await Mediator.Send(
                   new DeleteUserCommandRequest()
                   {
                       EmailAddress = emailAddress
                   }, cancellation);
                if (result.Status == Status.Error)
                {
                    return BadRequest(result.Message);
                }
                return Ok(result.Message);
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Critical, $"trace stack: {ex.Message}, {ex.InnerException}");
                return BadRequest(ErrorResource.General_Error);
            }
        }
    }
}
