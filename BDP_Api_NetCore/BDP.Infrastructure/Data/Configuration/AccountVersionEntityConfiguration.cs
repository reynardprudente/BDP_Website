using BDP.Domain.Entities.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDP.Infrastructure.Data.Configuration
{
    public class AccountVersionEntityConfiguration : IEntityTypeConfiguration<AccountVersionEntity>
    {
        public void Configure(EntityTypeBuilder<AccountVersionEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedBy)
            .HasMaxLength(100);

            builder.Property(x => x.ModifiedBy)
            .HasMaxLength(100);
        }
    }
}
