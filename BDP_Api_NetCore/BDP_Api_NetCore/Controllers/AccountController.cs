using BDP.Application.Command.Request.Account;
using BDP.Application.Dto;
using BDP.Application.Enum;
using BDP.Application.Query.Request.User;
using BDP.Domain.Enum;
using BDP_Api_NetCore.Model.Account;
using BDP_Api_NetCore.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace BDP_Api_NetCore.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddAccount([FromBody] AccountModel parameter, CancellationToken cancellationToken)
        {
            try
            {
                var account = Mapper.Map<AccountDTO>(parameter);
                var result = await Mediator.Send(
                 new AddAccountCommandRequest()
                 {
                     Account = account,
                     ClaimsPrincipal = User
                 }, cancellationToken);

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
