using BDP.Application.ViewModel.User;
using BDP.Application.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDP.Application.ViewModel.Account;
using System.Security.Claims;

namespace BDP.Application.Query.Request.Transaction
{
    public class CheckBalanceQueryRequest : IRequest<ResponseDTOViewModel<AccountDTOViewModel>>
    {
        public long AccountNumber { get; set; }

        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
