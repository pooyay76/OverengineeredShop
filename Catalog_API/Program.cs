using Catalog.Api.Contracts.Interfaces;
using Catalog.Api.Data;
using Catalog.Api.External.Client;
using Catalog.Api.External.MessageBroker;
using Catalog.Api.External.Server;
using Catalog.Api.Middlewares;
using Catalog.Api.Queries.NewArrivals;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("PostgreSQL");
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddScoped<IMediaClient, MediaClient>();
            builder.Services.AddScoped<IWarehouseClient, WarehouseClient>();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(NewArrivalsRequestHandler).Assembly));
            builder.Services.AddSingleton<IKafkaProducerService,KafkaProducerService>();
            builder.Services.AddDbContext<CatalogContext>(x => x.UseNpgsql(connectionString));
            builder.Services.AddGrpc();
            builder.Services.AddGrpcClient<MediaServices.MediaServicesClient>(x=>
            {
                x.Address=new Uri(builder.Configuration.GetSection("External")["MediaHost"]);
            });
            builder.Services.AddGrpcClient<WarehouseServices.WarehouseServicesClient>(x =>
            {
                x.Address = new Uri(builder.Configuration.GetSection("External")["WarehouseHost"]);
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline
            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.UseAuthorization();
            app.MapGrpcService<CatalogExternalServices>();
            app.MapControllers();



            app.Run();
        }
    }
}
