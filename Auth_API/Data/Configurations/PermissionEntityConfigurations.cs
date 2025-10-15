using Auth_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth_API.Data.Configurations
{
    public class PermissionEntityConfigurations : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Roles).WithMany(x => x.Permissions)
             .UsingEntity<RolePermission>();

            builder.Property(x => x.Title).HasMaxLength(30).IsRequired();

            builder.Property(x => x.Value).HasMaxLength(30).IsRequired();


        }

    }
}
