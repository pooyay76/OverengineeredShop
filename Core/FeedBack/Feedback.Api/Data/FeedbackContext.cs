using Common.Infrastructure.Persistence;
using Common.Infrastructure.Persistence.Interceptors;
using Feedback.Api.Data.Configurations;
using Feedback.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Api.Data
{
    public class FeedbackContext : ShopDbContextBase
    {
        public DbSet<Comment> Comments { get; set; }
        public FeedbackContext(DbContextOptions options, EventPublisherInterceptor interceptor) : base(options, interceptor)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommentTypeConfigurations).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
