using AutoMapper;
using BDP.Application.Dto;
using BDP.Application.ViewModel.User;
using BDP.Domain.Entities.User;
using BDP_Api_NetCore.Model.User;
using BDP_Api_NetCore.ViewModel.User;

namespace BDP_Api_NetCore.Mapper.User
{
    //<summary>
    //Mapping profile for user models
    //</summary>
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, UserDTO>()
                .ReverseMap();

            CreateMap<UserEntity, UserDTOViewModel>()
                .ForMember(d => d.Role, o => o.MapFrom(frm => frm.RoleId.ToString()));


            CreateMap<UserDTOViewModel, UserViewModel>();
        }
    }
}
