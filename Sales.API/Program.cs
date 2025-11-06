using Sales.Api.BackgroundServices;
using Sales.Application.ShoppingCartUseCases.Commands;
using Sales.Infrastructure;
using Sales.Infrastructure.Configurations;
using Sales.Infrastructure.External.Client.Catalog;
using System.Reflection;

namespace Sales.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("PostgreSQL");
            var catalogUri = new Uri(builder.Configuration.GetSection("External")["CatalogHost"]);
            // Add services to the container.

            builder.Services.RegisterInfrastuctureServices(builder.Configuration);
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddOptions();
            builder.Services.AddHostedService<KafkaConsumerHostedService>();
            builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(UpdateShoppingCartCommandHandler).Assembly));
            builder.Services.Configure<ZarinPalOptions>(builder.Configuration.GetSection("ZarinPal"));
            builder.Services.AddGrpcClient<CatalogServices.CatalogServicesClient>(x =>
            {
                x.Address = catalogUri;
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
