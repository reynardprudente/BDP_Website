using BDP.Application.ViewModel;
using MediatR;

namespace BDP.Application.Command.Request.User
{
    public class DeleteUserCommandRequest : IRequest<ResponseDTOViewModel<bool>>
    {
        public string EmailAddress { get; set; }
    }
}
