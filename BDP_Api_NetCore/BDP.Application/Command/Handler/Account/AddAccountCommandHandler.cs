using BDP.Application.Command.Request.Account;
using BDP.Application.Command.Request.User;
using BDP.Application.Constant;
using BDP.Application.Enum;
using BDP.Application.Helpers;
using BDP.Application.ViewModel;
using BDP.Domain.Entities.Account;
using BDP.Domain.Enum;
using BDP.Infrastructure.Data;
using BDP.Infrastructure.Data.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDP.Application.Command.Handler.Account
{
    public class AddAccountCommandHandler : IRequestHandler<AddAccountCommandRequest, ResponseDTOViewModel<bool>>
    {
        private readonly IGenericRepository genericRepository;
        private readonly IUserRepository userRepository;

        public AddAccountCommandHandler(IGenericRepository genericRepository, IUserRepository userRepository)
        {
            this.genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(genericRepository));
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        public async Task<ResponseDTOViewModel<bool>> Handle(AddAccountCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                request = request ?? throw new ArgumentNullException(nameof(request));

                var user = await userRepository.GetUserByEmail(request.Account.EmailAddress, cancellationToken);
                if (user == null || user.RoleId != Roles.Customer)
                {
                    return new ResponseDTOViewModel<bool>()
                    {
                        Status = Status.Error,
                        Message = ErrorResource.EmailAddress_Invalid
                    };
                }
               
                if (user.Account != null)
                {
                    return new ResponseDTOViewModel<bool>()
                    {
                        Status = Status.Error,
                        Message = ErrorResource.EmailAddress_HasAccount
                    };
                } 
                await using var transaction = await genericRepository.BeginTransactionAsync(cancellationToken);

                var userClaims = request.ClaimsPrincipal.Claims.Any() ? request.ClaimsPrincipal.Claims.First(x => x.Type.Contains("emailaddress")).Value
                    : null;

                var account = new AccountEntity()
                {
                    AccountNumber = Randomizer.Numeric(10),
                    CreatedBy = userClaims ?? SystemConstant.SuperAdmin().EmailAddress
                };
                genericRepository.Add(account);

                var accountVersion = new AccountVersionEntity()
                {
                    Account = account,
                    Amount = request.Account.Amount,
                    CreatedBy = userClaims ?? SystemConstant.SuperAdmin().EmailAddress,
                    CreatedDate = DateTime.Now,
                };

                genericRepository.Add(accountVersion);
                user.Account = account;

                genericRepository.SaveChanges();
                await transaction.CommitAsync();

                return new ResponseDTOViewModel<bool>()
                {
                    Status = Status.Success,
                    Value = true,
                    Message = "Account successfully inserted"
                };

                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorResource.AddAccountHandlerError, ex);
            }
          
        }
    }
}
