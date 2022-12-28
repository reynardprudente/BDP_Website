using BDP.Application.ViewModel;
using BDP.Application.ViewModel.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDP.Application.Query.Request.User
{
    public class GetUsersQueryRequest : IRequest<ResponseDTOViewModel<List<UserDTOViewModel>>>
    {
    }
}
