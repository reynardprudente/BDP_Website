using BDP.Domain.Entities.Account;
using BDP.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace BDP.Infrastructure.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<AccountEntity> Account { get; set; }

        public DbSet<AccountVersionEntity> AccountVersion { get; set; }
    }
}
