using BDP.Application.Command.Request.User;
using BDP.Application.Enum;
using BDP.Application.ViewModel;
using BDP.Infrastructure.Data.Interface;
using MediatR;

namespace BDP.Application.Command.Handler.User
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommandRequest, ResponseDTOViewModel<bool>>
    {
        private readonly IUserRepository userRepository;
        private readonly IGenericRepository genericRepository;
        public DeleteUserCommandHandler(IUserRepository userRepository, IGenericRepository genericRepository)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(genericRepository));
        }
        public async Task<ResponseDTOViewModel<bool>> Handle(DeleteUserCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                request = request ?? throw new ArgumentNullException(nameof(request));

                if (request.EmailAddress == null)
                {
                    return new ResponseDTOViewModel<bool>()
                    {
                        Status = Status.Error,
                        Message = string.Format(ErrorResource.Cannot_Be_Empty, nameof(request.EmailAddress))
                    };
                }

                await using var transaction = await genericRepository.BeginTransactionAsync(cancellationToken);
                var user = await userRepository.GetUserByEmail(request.EmailAddress, cancellationToken);

                if (user == null)
                {
                    return new ResponseDTOViewModel<bool>()
                    {
                        Status = Status.Error,
                        Message = ErrorResource.User_NotExist
                    };
                }

                genericRepository.Delete(user);
                genericRepository.SaveChanges();
                await transaction.CommitAsync();


                return new ResponseDTOViewModel<bool>()
                {
                    Status = Status.Success,
                    Value = true,
                    Message = "User successfully deleted"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorResource.DeleteUserHandlerError, ex);
            }
        }
    }
}
