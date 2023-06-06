using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Persistence.Context.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);

            builder.OwnsOne(u => u.Email, map =>
            {
                map.Property(e => e.Address).HasColumnName("Email");
                map.Property(e => e.Verified).HasColumnName("EmailVerified");
            });
            builder.OwnsOne(u => u.Password, map =>
            {
                map.Property(p => p.Hash).HasColumnName("PasswordHash");
                map.Property(p => p.Salt).HasColumnName("PasswordSalt");
            });

            builder.HasOne(u => u.Profile).WithOne(p => p.User).HasForeignKey<Profile>(p => p.UserId);
        }
    }
}
