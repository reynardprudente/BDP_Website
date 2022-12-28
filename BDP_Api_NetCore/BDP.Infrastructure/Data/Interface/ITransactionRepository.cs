using BDP.Domain.Entities.Account;
using BDP.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDP.Infrastructure.Data.Interface
{
    public interface ITransactionRepository
    {
        Task<List<AccountVersionEntity>> GetAccountByAccountNumber(long accountNumber, CancellationToken cancellationToken);
    }
}
