using Media.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Media.Api.Data
{
    public class MediaDbContext : DbContext
    {

        public DbSet<MediaEntity> Medias { get; set; }

        public MediaDbContext(DbContextOptions options) : base(options)
        {
        }


    }
}
