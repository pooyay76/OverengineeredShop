using Auth.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Api.Data.Configurations
{

    public class RoleEntityConfigurations : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Permissions).WithMany(x => x.Roles)
                .UsingEntity<RolePermission>();

            builder.HasMany(x => x.Users).WithMany(x => x.Roles)
                .UsingEntity<UserRole>();

            builder.Property(x => x.Title).HasMaxLength(40).IsRequired();
        }
    }
}
