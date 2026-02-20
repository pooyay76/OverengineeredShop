using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GlobalEventStoreWorker.Data
{
    //THERE IS A CONNECTION STRING VALUE HARDCODED IN THIS CLASS, IF MIGRATIONS DONT WORK CHECK THIS CLASS FIRST
    //DONT INJECT OR USE THIS CLASS ANYWHERE this is for ef core design time and not referenced anywhere else in the codebase
    public class GlobalEventDbContextFactory : IDesignTimeDbContextFactory<GlobalEventContext> 
    {

        public GlobalEventContext CreateDbContext(string[] args) 
        {

            //var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";

            //var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json").AddJsonFile($"appsettings.{environment}.json", optional: true)
            //    .Build();

            //Console.WriteLine($"Using environment: {environment}");
            var connectionString =
                "Host=localhost;Database=OverengineeredGlobalEvents;Username=postgres;Password=admin";
            //string connectionString = config.GetConnectionString("Default");

            var options = new DbContextOptionsBuilder<GlobalEventContext>()
                .UseNpgsql(connectionString).Options; 
            return new GlobalEventContext(options);
        } 
    }
}
