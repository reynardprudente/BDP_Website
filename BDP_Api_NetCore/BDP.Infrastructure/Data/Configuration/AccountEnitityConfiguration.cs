using BDP.Domain.Entities.Account;
using BDP.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDP.Infrastructure.Data.Configuration
{
    public class AccountEnitityConfiguration : IEntityTypeConfiguration<AccountEntity>
    {
        public void Configure(EntityTypeBuilder<AccountEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedBy)
              .HasMaxLength(100);

            builder.Property(x => x.AccountNumber)
              .HasMaxLength(30);
        }
    }
}
