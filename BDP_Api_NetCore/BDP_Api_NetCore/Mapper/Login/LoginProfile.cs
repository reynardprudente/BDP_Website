using AutoMapper;
using BDP.Application.Dto;
using BDP_Api_NetCore.Model.Login;

namespace BDP_Api_NetCore.Mapper.Login
{
    //<summary>
    //Mapping profile for login models
    //</summary>
    public class LoginProfile : Profile
    {
        public LoginProfile()
        {
            CreateMap<LoginModel, LoginDTO>();
        }
    }
}
