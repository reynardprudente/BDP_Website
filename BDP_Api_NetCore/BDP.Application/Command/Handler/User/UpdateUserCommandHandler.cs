using BDP.Application.Command.Request.User;
using BDP.Application.Enum;
using BDP.Application.ViewModel;
using BDP.Infrastructure.Data.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDP.Application.Command.Handler.User
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, ResponseDTOViewModel<bool>>
    {
        private readonly IUserRepository userRepository;
        private readonly IGenericRepository genericRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository, IGenericRepository genericRepository)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(genericRepository));
        }
        public async Task<ResponseDTOViewModel<bool>> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                request = request ?? throw new ArgumentNullException(nameof(request));
                if (request.user.EmailAddress == null)
                {
                    return new ResponseDTOViewModel<bool>()
                    {
                        Status = Status.Error,
                        Message = string.Format(ErrorResource.Cannot_Be_Empty, nameof(request.user.EmailAddress))
                    };
                }
                await using var transaction = await genericRepository.BeginTransactionAsync(cancellationToken);

                var userClaims = request.ClaimsPrincipal.Claims.Any() ? request.ClaimsPrincipal.Claims.First(x => x.Type.Contains("emailaddress")).Value
                    : null;


                var user = await userRepository.GetUserByEmail(request.user.EmailAddress, cancellationToken);
                
                if(user == null)
                {
                    return new ResponseDTOViewModel<bool>()
                    {
                        Status = Status.Error,
                        Message = ErrorResource.User_NotExist
                    };
                }
                user.FirstName = request.user.FirstName ?? user.FirstName;
                user.LastName = request.user.LastName ?? user.LastName;
                user.MiddleName = request.user.MiddleName ?? user.MiddleName;
                user.ModifiedBy = userClaims;

                genericRepository.SaveChanges();
                await transaction.CommitAsync();

                return new ResponseDTOViewModel<bool>()
                {
                    Status = Status.Success,
                    Value = true,
                    Message = "User successfully updated"
                };

            }
            catch (Exception ex)
            {
                throw new Exception(ErrorResource.UpdateUserHandlerError, ex);
            }
        }
    }
}
