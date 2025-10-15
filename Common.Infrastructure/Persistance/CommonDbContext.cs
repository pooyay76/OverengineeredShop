using Media_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Persistance
{
    public class CommonDbContext : DbContext
    {
        public DbSet<Media> Medias { get; set; }
        public CommonDbContext(DbContextOptions options) : base(options)
        {
        }

        protected CommonDbContext()
        {
        }
    }
}
