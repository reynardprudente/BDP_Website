using BDP.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDP.Infrastructure.Data.Configuration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
           builder.HasKey(x => x.Id);

           builder.Property(x => x.FirstName)
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .HasMaxLength(50);

            builder.Property(x => x.MiddleName)
                .HasMaxLength(20);

            builder.Property(x => x.EmailAddress)
            .HasMaxLength(100);

            builder.Property(x => x.CreatedBy)
                .HasMaxLength(100);


            builder.Property(x => x.ModifiedBy)
                .HasMaxLength(100);
        }
    }
}
