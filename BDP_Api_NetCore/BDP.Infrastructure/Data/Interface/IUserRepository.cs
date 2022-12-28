using BDP.Domain.Entities.Account;
using BDP.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDP.Infrastructure.Data.Interface
{
    public interface IUserRepository
    {
        Task<List<UserEntity>> GetUsers(CancellationToken cancellationToken);

        Task<UserEntity> GetUserByEmail(string email, CancellationToken cancellationToken);

        Task<UserEntity> GetUserByAccountId(AccountEntity account, CancellationToken cancellationToken);
    }
}
