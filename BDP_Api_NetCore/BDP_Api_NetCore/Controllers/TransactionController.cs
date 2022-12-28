using BDP.Application.Command.Request.Transaction;
using BDP.Application.Dto;
using BDP.Application.Enum;
using BDP.Application.Query.Request.Transaction;
using BDP.Application.Query.Request.User;
using BDP.Domain.Enum;
using BDP_Api_NetCore.Model.Transaction;
using BDP_Api_NetCore.ViewModel.Account;
using BDP_Api_NetCore.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BDP_Api_NetCore.Controllers
{
    [Authorize(Roles = "Customer")]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ApiControllerBase
    {
        [HttpGet("checkbalance")]
        public async Task<IActionResult> CheckBalance(long accountNumber, CancellationToken cancellation)
        {
            try
            {
                var result = await Mediator.Send(
                new CheckBalanceQueryRequest()
                {
                    AccountNumber = accountNumber,
                    ClaimsPrincipal = User 
                }
                , cancellation);
                if (result.Status == Status.Error)
                {
                    return BadRequest(result.Message);
                }
                var account = Mapper.Map<AccountViewModel>(result.Value);
                return Ok(account);
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Critical, $"trace stack: {ex.Message}, {ex.InnerException}");
                return BadRequest(ErrorResource.General_Error);
            }
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] TransactionModel parameter, CancellationToken cancellation)
        {
            try
            {
                var transaction = Mapper.Map<TransactionDTO>(parameter);
                var result = await Mediator.Send(
                    new DepositCommandRequest()
                    {
                        Transaction = transaction,
                        ClaimsPrincipal = User
                    }, cancellation
                 );

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

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] TransactionModel parameter, CancellationToken cancellation)
        {
            try
            {
                var transaction = Mapper.Map<TransactionDTO>(parameter);
                var result = await Mediator.Send(
                    new WithdrawCommandRequest()
                    {
                        Transaction = transaction,
                        ClaimsPrincipal = User
                    }, cancellation
                 );

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

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferModel parameter, CancellationToken cancellation)
        {
            try
            {
                var transaction = Mapper.Map<TransferDTO>(parameter);
                var result = await Mediator.Send(
                    new TransferCommandRequest()
                    {
                        Transaction = transaction,
                        ClaimsPrincipal = User
                    }, cancellation
                 );

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
