
using Catalog_API.Data;
using Catalog_API.Middlewares;
using Microsoft.EntityFrameworkCore;

namespace Catalog_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("default");
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<CatalogContext>(x => x.UseSqlServer(connectionString));



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


            app.MapControllers();



            app.Run();
        }
    }
}
