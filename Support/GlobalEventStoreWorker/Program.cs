using GlobalEventStoreWorker.Data;
using Microsoft.EntityFrameworkCore;

namespace GlobalEventStoreWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("PostgreSQL");
            builder.Services.AddDbContext<GlobalEventContext>(x=>x.UseNpgsql(connectionString));
            builder.Services.AddHostedService<KafkaEventConsumerWorker>();
            var host = builder.Build();
            host.Run();
        }
    }
}