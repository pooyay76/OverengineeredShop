using Media.Api.Data;
using Media.Api.External.Server;
using Media.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Media.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions
            {
                Args = args,
                ContentRootPath = Directory.GetCurrentDirectory(),
                WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "Medias")
            });
            string connectionString = builder.Configuration.GetConnectionString("Default");

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient<IFileService, FileService>();
            builder.Services.AddDbContext<MediaDbContext>(x => x.UseNpgsql(connectionString));
            builder.Services.AddGrpc();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Medias")),
                RequestPath = "/Medias"
            });
            app.UseAuthorization();
            app.MapGrpcService<MediaExternalServices>();

            app.MapControllers();

            app.Run();
        }
    }
}
