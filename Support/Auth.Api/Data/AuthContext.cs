using Auth.Api.Data.Configurations;
using Auth.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.Api.Data
{
    public class AuthContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        public AuthContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEntityConfigurations).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
