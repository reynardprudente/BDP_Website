using BDP.Application.Command.Request.User;
using BDP.Application.Constant;
using BDP.Application.Enum;
using BDP.Application.Helpers;
using BDP.Application.ViewModel;
using BDP.Domain.Entities.User;
using BDP.Infrastructure.Data.Interface;
using MediatR;

namespace BDP.Application.Command.Handler.User
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommandRequest, ResponseDTOViewModel<bool>>
    {
        private readonly IUserRepository userRepository;
        private readonly IGenericRepository genericRepository;

        public AddUserCommandHandler(IUserRepository userRepository, IGenericRepository genericRepository)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(genericRepository));
        }
        public async Task<ResponseDTOViewModel<bool>> Handle(AddUserCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                request = request ?? throw new ArgumentNullException(nameof(request));
                var user = await userRepository.GetUserByEmail(request.user.EmailAddress, cancellationToken);
                if (user != null)
                {
                    return new ResponseDTOViewModel<bool>()
                    {
                        Status = Status.Error,
                        Message = ErrorResource.User_AlreadyExist
                    };
                }
                await using var transaction = await genericRepository.BeginTransactionAsync(cancellationToken);

                var userClaims = request.ClaimsPrincipal.Claims.Any() ? request.ClaimsPrincipal.Claims.First(x => x.Type.Contains("emailaddress")).Value
                    : null;

                var salt = Encryption.GenerateSalt();
                var newUser = new UserEntity()
                {
                    RoleId = request.user.Role,
                    FirstName = request.user.FirstName,
                    MiddleName = request.user.MiddleName,
                    LastName = request.user.LastName,
                    EmailAddress = request.user.EmailAddress,
                    PasswordSalt = Convert.ToBase64String(salt),
                    PasswordHash = Encryption.Hash(request.user.Password, salt),
                    CreatedBy = userClaims ?? SystemConstant.SuperAdmin().EmailAddress
                };

                genericRepository.Add(newUser);
                genericRepository.SaveChanges();
                await transaction.CommitAsync();

                return new ResponseDTOViewModel<bool>()
                {
                    Status = Status.Success,
                    Value = true,
                    Message = "User successfully inserted"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorResource.AddUserHandlerError, ex);
            }
        }
    }
}
