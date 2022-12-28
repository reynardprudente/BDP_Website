using AutoMapper;
using BDP.Application.Dto;
using BDP_Api_NetCore.Model.Login;
using BDP_Api_NetCore.Model.Transaction;

namespace BDP_Api_NetCore.Mapper.Transaction
{
    //<summary>
    //Mapping profile for trasaction models
    //</summary>
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<TransactionModel, TransactionDTO>();

            CreateMap<TransferModel, TransferDTO>();
        }
    }
}
