using Auth_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth_API.Data.Configurations
{
    public class UserEntityConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).HasMaxLength(55).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(55).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(90).IsRequired();
            builder.Property(x => x.PhoneNumber).HasMaxLength(12).IsRequired();
            builder.Property(x => x.SmsCode).HasMaxLength(6).IsRequired(false);


            builder.HasMany(x => x.Roles).WithMany(x => x.Users)
                .UsingEntity<UserRole>();
        }
    }
}
