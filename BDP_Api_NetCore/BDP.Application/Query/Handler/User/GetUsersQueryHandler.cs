using BDP.Application.ViewModel.User;
using BDP.Application.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDP.Infrastructure.Data.Interface;
using BDP.Application.Enum;
using AutoMapper;
using BDP.Application.Dto;
using System.Reflection.Metadata;
using BDP.Application.Query.Request.User;

namespace BDP.Application.Query.Handler.User
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQueryRequest, ResponseDTOViewModel<List<UserDTOViewModel>>>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<ResponseDTOViewModel<List<UserDTOViewModel>>> Handle(GetUsersQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var usersFromDb = await userRepository.GetUsers(cancellationToken);

                var users = mapper.Map<List<UserDTOViewModel>>(usersFromDb);
                return new ResponseDTOViewModel<List<UserDTOViewModel>>
                {
                    Status = Status.Success,
                    Value = users,
                    Message = "Get all user successfully"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorResource.GetUsersHandlerError, ex);
            }
        }
    }
}
