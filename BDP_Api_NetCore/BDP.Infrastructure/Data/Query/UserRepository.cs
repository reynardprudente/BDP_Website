using BDP.Domain.Entities.Account;
using BDP.Domain.Entities.User;
using BDP.Infrastructure.Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDP.Infrastructure.Data.Query
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext databaseContext;

        public UserRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
        }

        public async Task<UserEntity> GetUserByAccountId(AccountEntity account, CancellationToken cancellationToken)
        {
            return await this.databaseContext.Users
               .FirstOrDefaultAsync(x => x.Account == account);
        }

        public async Task<UserEntity> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            return await this.databaseContext.Users
                .Include(x => x.Account)
                .FirstOrDefaultAsync(x => x.EmailAddress == email);
        }

        public async Task<List<UserEntity>> GetUsers(CancellationToken cancellationToken)
        {
            return await this.databaseContext.Users.Include(x => x.Account).ToListAsync(cancellationToken);
        }
    }
}
