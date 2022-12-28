using BDP.Application.Dto;
using BDP.Application.ViewModel;
using MediatR;
using System.IdentityModel.Tokens.Jwt;

namespace BDP.Application.Command.Request.Login
{
    public class LoginCommandRequest : IRequest<ResponseDTOViewModel<JwtSecurityToken>>
    {
        public LoginDTO login { get; set; }
    }
}
