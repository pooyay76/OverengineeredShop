using Common.Domain.Models;
using GlobalEventStoreWorker.Models;
using Microsoft.EntityFrameworkCore;

namespace GlobalEventStoreWorker.Data
{
    public class GlobalEventContext : DbContext
    {

        public DbSet<GlobalEventEntity> GlobalEvents { get; set; }



        public GlobalEventContext(DbContextOptions options) : base(options)
        {
        }
    }
}
