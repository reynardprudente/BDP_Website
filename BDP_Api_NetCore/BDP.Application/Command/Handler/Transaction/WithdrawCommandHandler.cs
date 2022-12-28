using BDP.Application.Command.Request.Transaction;
using BDP.Application.Enum;
using BDP.Application.ViewModel;
using BDP.Domain.Entities.Account;
using BDP.Infrastructure.Data;
using BDP.Infrastructure.Data.Interface;
using MediatR;
using System.Net.Http.Headers;

namespace BDP.Application.Command.Handler.Transaction
{
    public class WithdrawCommandHandler : IRequestHandler<WithdrawCommandRequest, ResponseDTOViewModel<bool>>
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IGenericRepository genericRepository;
        private readonly IUserRepository userRepository;

        public WithdrawCommandHandler(ITransactionRepository transactionRepository, IGenericRepository genericRepository,
            IUserRepository userRepository)
        {
            this.transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            this.genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(genericRepository));
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository)); ;
        }
        public async Task<ResponseDTOViewModel<bool>> Handle(WithdrawCommandRequest request, CancellationToken cancellationToken)
        {
			try
			{
                request = request ?? throw new ArgumentNullException(nameof(request));

                if (request.Transaction.AccountNumber == 0)
                {
                    return new ResponseDTOViewModel<bool>
                    {
                        Status = Status.Error,
                        Message = ErrorResource.InvalidAccountNumber,
                    };
                }
                var accountVersionFromDB = await this.transactionRepository.GetAccountByAccountNumber(request.Transaction.AccountNumber, cancellationToken);            
               
                if (accountVersionFromDB == null || !accountVersionFromDB.Any())
                {
                    return InvalidEmailResponse();
                }

                var accountUser = await this.userRepository.GetUserByAccountId(accountVersionFromDB.FirstOrDefault().Account, cancellationToken);
                var userClaims = request.ClaimsPrincipal.Claims.Any() ? request.ClaimsPrincipal.Claims.First(x => x.Type.Contains("emailaddress")).Value
                    : null;

                if (accountUser.EmailAddress != userClaims)
                {
                    return InvalidEmailResponse();
                }

                await using var transaction = await genericRepository.BeginTransactionAsync(cancellationToken);

                var accountVersionLatest = accountVersionFromDB.OrderByDescending(x => x.ModiefiedDate).FirstOrDefault();

                if(request.Transaction.Amount > accountVersionLatest.Amount)
                {
                    return new ResponseDTOViewModel<bool>()
                    {
                        Status = Status.Error,
                        Message = ErrorResource.Insufficient_Balance
                    };
                }

                var accountVersion = new AccountVersionEntity()
                {
                    Account = accountVersionLatest.Account,
                    Amount = accountVersionLatest.Amount - request.Transaction.Amount,
                    CreatedBy = null,
                    CreatedDate = null,
                    ModifiedBy = userClaims,
                    ModiefiedDate = DateTime.Now
                };

                genericRepository.Add(accountVersion);
                genericRepository.SaveChanges();
                await transaction.CommitAsync();

                return new ResponseDTOViewModel<bool>()
                {
                    Status = Status.Success,
                    Value = true,
                    Message = $"Successfully withdraw amount of {request.Transaction.Amount} php on {request.Transaction.AccountNumber} account number"
                };
            }
			catch (Exception ex)
			{
                throw new Exception(ErrorResource.WithdrawHandlerError, ex);
            }
        }

        private ResponseDTOViewModel<bool> InvalidEmailResponse()
        {
            return new ResponseDTOViewModel<bool>
            {
                Status = Status.Error,
                Message = ErrorResource.InvalidAccountNumber,
            };
        }
    }
}
