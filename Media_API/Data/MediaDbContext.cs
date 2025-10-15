using Media_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Media_API.Data
{
    public class MediaDbContext : DbContext
    {

        public DbSet<Media> Medias { get; set; }

        public MediaDbContext(DbContextOptions options) : base(options)
        {
        }


    }
}
