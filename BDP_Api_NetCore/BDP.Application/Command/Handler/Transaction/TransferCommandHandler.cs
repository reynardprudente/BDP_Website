using BDP.Application.Command.Request.Transaction;
using BDP.Application.Enum;
using BDP.Application.ViewModel;
using BDP.Domain.Entities.Account;
using BDP.Infrastructure.Data.Interface;
using BDP.Infrastructure.Data.Query;
using MediatR;

namespace BDP.Application.Command.Handler.Transaction
{
    public class TransferCommandHandler : IRequestHandler<TransferCommandRequest, ResponseDTOViewModel<bool>>
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IGenericRepository genericRepository;
        private readonly IUserRepository userRepository;

        public TransferCommandHandler(ITransactionRepository transactionRepository, IGenericRepository genericRepository,
            IUserRepository userRepository)
        {
            this.transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            this.genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(genericRepository));
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository)); ;
        }
        public async Task<ResponseDTOViewModel<bool>> Handle(TransferCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                request = request ?? throw new ArgumentNullException(nameof(request));

                if (request.Transaction.AccountNumberOrigin == 0 || request.Transaction.AccountNumberDestination == 0
                    || request.Transaction.AccountNumberOrigin == request.Transaction.AccountNumberDestination)
                {
                    return new ResponseDTOViewModel<bool>
                    {
                        Status = Status.Error,
                        Message = ErrorResource.InvalidAccountNumber,
                    };
                }

                var accountVersionOriginFromDB = await this.transactionRepository.GetAccountByAccountNumber(request.Transaction.AccountNumberOrigin, cancellationToken);
                var accountVersionDestinationFromDB = await this.transactionRepository.GetAccountByAccountNumber(request.Transaction.AccountNumberDestination, cancellationToken);

                if (accountVersionOriginFromDB == null || !accountVersionOriginFromDB.Any()
                    || accountVersionDestinationFromDB == null || !accountVersionDestinationFromDB.Any())
                {
                    return InvalidEmailResponse();
                }

                var accountUser = await this.userRepository.GetUserByAccountId(accountVersionOriginFromDB.FirstOrDefault().Account, cancellationToken);
                var userClaims = request.ClaimsPrincipal.Claims.Any() ? request.ClaimsPrincipal.Claims.First(x => x.Type.Contains("emailaddress")).Value
                    : null;

                if(accountUser.EmailAddress != userClaims)
                {
                    return InvalidEmailResponse();
                }

                await using var transaction = await genericRepository.BeginTransactionAsync(cancellationToken);


                var accountVersionLatestOrigin = accountVersionOriginFromDB.OrderByDescending(x => x.ModiefiedDate).FirstOrDefault();
                var accountVersionLatestDestination = accountVersionDestinationFromDB.OrderByDescending(x => x.ModiefiedDate).FirstOrDefault();

                if (request.Transaction.Amount > accountVersionLatestOrigin.Amount)
                {
                    return new ResponseDTOViewModel<bool>()
                    {
                        Status = Status.Error,
                        Message = ErrorResource.Insufficient_Balance
                    };
                }

                var accountVersionOrigin = new AccountVersionEntity()
                {
                    Account = accountVersionLatestOrigin.Account,
                    Amount = accountVersionLatestOrigin.Amount - request.Transaction.Amount,
                    CreatedBy = null,
                    CreatedDate = null,
                    ModifiedBy = userClaims,
                    ModiefiedDate = DateTime.Now
                };

                genericRepository.Add(accountVersionOrigin);

                var accountVersionDestination = new AccountVersionEntity()
                {
                    Account = accountVersionLatestDestination.Account,
                    Amount = accountVersionLatestDestination.Amount + request.Transaction.Amount,
                    CreatedBy = null,
                    CreatedDate = null,
                    ModifiedBy = userClaims,
                    ModiefiedDate = DateTime.Now
                };

                genericRepository.Add(accountVersionDestination);

                genericRepository.SaveChanges();
                await transaction.CommitAsync();

                return new ResponseDTOViewModel<bool>()
                {
                    Status = Status.Success,
                    Value = true,
                    Message = $"Successfully transfer amount of {request.Transaction.Amount} php from {request.Transaction.AccountNumberOrigin} to {request.Transaction.AccountNumberDestination} account number"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorResource.TransferHandleError, ex);
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
