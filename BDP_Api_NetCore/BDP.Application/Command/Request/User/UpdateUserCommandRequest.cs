using BDP.Application.Dto;
using BDP.Application.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BDP.Application.Command.Request.User
{
    public class UpdateUserCommandRequest : IRequest<ResponseDTOViewModel<bool>>
    {
        public UserDTO user { get; set; }

        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
