
using Media_API.Data;
using Media_API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Media_API
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

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient<IFileService, FileService>();
            string connectionString = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<MediaDbContext>(x => x.UseSqlServer(connectionString));
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


            app.MapControllers();

            app.Run();
        }
    }
}
