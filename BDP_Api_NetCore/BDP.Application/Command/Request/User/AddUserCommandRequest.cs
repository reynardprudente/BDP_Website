using BDP.Application.Dto;
using BDP.Application.ViewModel;
using MediatR;
using System.Security.Claims;

namespace BDP.Application.Command.Request.User
{
    public class AddUserCommandRequest : IRequest<ResponseDTOViewModel<bool>>
    {
        public UserDTO user { get; set; }

        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
