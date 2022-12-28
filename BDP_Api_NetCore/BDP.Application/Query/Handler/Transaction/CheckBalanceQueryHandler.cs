using BDP.Application.Query.Request.User;
using BDP.Application.ViewModel.User;
using BDP.Application.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDP.Application.Query.Request.Transaction;
using BDP.Application.ViewModel.Account;
using BDP.Infrastructure.Data.Interface;
using BDP.Application.Enum;
using AutoMapper;
using System.Security.Principal;

namespace BDP.Application.Query.Handler.Transaction
{
    public class CheckBalanceQueryHandler : IRequestHandler<CheckBalanceQueryRequest, ResponseDTOViewModel<AccountDTOViewModel>>
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public CheckBalanceQueryHandler(ITransactionRepository transactionRepository, IUserRepository userRepository, 
            IMapper mapper)
        {
            this.transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(transactionRepository)); ;
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<ResponseDTOViewModel<AccountDTOViewModel>> Handle(CheckBalanceQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                request = request ?? throw new ArgumentNullException(nameof(request));
                if(request.AccountNumber == 0)
                {
                    return new ResponseDTOViewModel<AccountDTOViewModel>
                    {
                        Status = Status.Error,
                        Message = ErrorResource.InvalidAccountNumber,
                    };
                }
                var accountVersionFromDB = await this.transactionRepository.GetAccountByAccountNumber(request.AccountNumber, cancellationToken);  

                if (accountVersionFromDB == null || !accountVersionFromDB.Any())
                {
                   return InvalidEmailResponse();
                }

                var accountUser = await this.userRepository.GetUserByAccountId(accountVersionFromDB.FirstOrDefault().Account, cancellationToken);

                var userClaims = request.ClaimsPrincipal.Claims.Any() ? request.ClaimsPrincipal.Claims.First(x => x.Type.Contains("emailaddress")).Value
                : null;

                if(accountUser.EmailAddress != userClaims)
                {
                    return InvalidEmailResponse();
                }

                var account = mapper.Map<AccountDTOViewModel>
                    (accountVersionFromDB.OrderByDescending(x => x.ModiefiedDate).FirstOrDefault());
                account.AccountNumber = request.AccountNumber;

                return new ResponseDTOViewModel<AccountDTOViewModel>
                {
                    Status = Status.Success,
                    Value = account,
                    Message = "Account by account number",
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorResource.CheckBalanceHandlerError, ex);
            }
        }

        private ResponseDTOViewModel<AccountDTOViewModel> InvalidEmailResponse()
        {
            return new ResponseDTOViewModel<AccountDTOViewModel>
            {
                Status = Status.Error,
                Message = ErrorResource.InvalidAccountNumber,
            };
        }
    }
}
