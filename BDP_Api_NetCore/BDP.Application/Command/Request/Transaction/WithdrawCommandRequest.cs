using BDP.Application.Dto;
using BDP.Application.ViewModel;
using MediatR;
using System.Security.Claims;

namespace BDP.Application.Command.Request.Transaction
{
    public class WithdrawCommandRequest : IRequest<ResponseDTOViewModel<bool>>
    {
        public TransactionDTO Transaction { get; set; }

        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
