using Catalog.Api.Contracts;
using Catalog.Api.Data;
using Catalog.Api.EventHandlers;
using Catalog.Api.External.Client;
using Catalog.Api.External.Server;
using Catalog.Api.Middlewares;
using Catalog.Api.Queries.NewArrivals;
using Common.Application.Contracts;
using Common.Application.Helpers;
using Common.Domain.Language.Catalog.Events.Global;
using Common.Domain.Language.Catalog.ValueObjects;
using Common.Infrastructure;
using Common.Infrastructure.Extensions;
using Common.Infrastructure.MessageBroker;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Catalog.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);




            var connectionString = builder.Configuration.GetConnectionString("PostgreSQL");

            // Add services to the container.
            CommonServiceRegistry.RegisterCommonServices(builder.Services, connectionString);

            builder.Services.AddControllers().AddJsonOptions(x => 
                {
                    foreach (var converter in EventSerializer.Options.Converters)
                    {
                        x.JsonSerializerOptions.Converters.Add(converter);
                    }

                });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddScoped<IMediaClient, MediaClient>();
            builder.Services.AddScoped<IWarehouseClient, WarehouseClient>();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(NewArrivalsRequestHandler).Assembly));
            builder.Services.AddSingleton<IEventProducerService,KafkaProducer>();
            builder.Services.RegisterMediatorHandlersFromAssembly(typeof(ProductItemCreatedEventHandler).Assembly);

            builder.Services.AddDbContext<CatalogContext>(x => x.UseNpgsql(connectionString));

            //registering grpc service
            builder.Services.AddGrpc();

            //registering clients
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

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

                app.UseMiddleware<ExceptionMiddleware>();


            app.UseHttpsRedirection();


            app.UseAuthorization();

            //Grpc middleware
            app.MapGrpcService<CatalogExternalServices>();

            app.MapControllers();



            app.Run();
        }
    }
}
