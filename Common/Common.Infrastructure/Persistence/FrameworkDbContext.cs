

using Common.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Persistence
{
    public class FrameworkDbContext : DbContext
    {
        public DbSet<EventEntity> Events { get; set; }
        public DbSet<OutboxEntity> OutboxEntities { get; set; }

        public FrameworkDbContext(DbContextOptions<FrameworkDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FrameworkDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
