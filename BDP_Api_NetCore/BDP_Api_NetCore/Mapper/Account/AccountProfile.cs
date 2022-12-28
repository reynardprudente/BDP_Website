using AutoMapper;
using BDP.Application.Dto;
using BDP.Application.ViewModel.Account;
using BDP.Domain.Entities.Account;
using BDP_Api_NetCore.Model.Account;
using BDP_Api_NetCore.Model.Login;
using BDP_Api_NetCore.ViewModel.Account;

namespace BDP_Api_NetCore.Mapper.Account
{
    //<summary>
    //Mapping profile for account models
    //</summary>
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<AccountEntity, AccountDTOViewModel>();

            CreateMap<AccountDTOViewModel, AccountViewModel>();

            CreateMap<AccountVersionEntity, AccountDTOViewModel>();

            CreateMap<AccountModel, AccountDTO>();
        }
    }
}
