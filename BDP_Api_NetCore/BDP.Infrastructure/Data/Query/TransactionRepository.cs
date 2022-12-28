using BDP.Domain.Entities.Account;
using BDP.Infrastructure.Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDP.Infrastructure.Data.Query
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DatabaseContext databaseContext;

        public TransactionRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
        }
        public async Task<List<AccountVersionEntity>> GetAccountByAccountNumber(long accountNumber, CancellationToken cancellationToken)
        {
            return await this.databaseContext.AccountVersion.Where(x => x.Account.AccountNumber == accountNumber)
                .Include(x => x.Account)
                .ToListAsync(cancellationToken);
        }
    }
}
