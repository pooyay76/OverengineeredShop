using Auth.Api.Contracts;
using Auth.Api.Infrastructure.Data;
using Auth.Api.Infrastructure.Dev;
using Auth.Api.Options;
using Auth.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Auth.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
           
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.ConfigureKestrel(options => { });





            #region Jwt

            //assign private key, from file in development, environment variable in production
            string keyPem;
            if (builder.Environment.IsDevelopment())
            {
                var filePath = Path.Combine("Infrastructure", "Dev", "private.pem");

                if(File.Exists(filePath)==false)
                    KeyFileGenerator.Generate();

                keyPem = File.ReadAllText(filePath);
            }
            else
            {
                    keyPem = Environment.GetEnvironmentVariable("JwtPrivateKey") ?? throw new ArgumentException(
                        "Security key must be assigned in environment variables for production environment");
            }

            var rsa = RSA.Create();
            rsa.ImportFromPem(keyPem);
            RsaSecurityKey rsaKey = new(rsa);
            SigningCredentials creds = new(rsaKey, SecurityAlgorithms.RsaSha256);
            builder.Services.AddSingleton<SigningCredentials>(creds);
            builder.Services.AddScoped<IPermissionService, PermissionService>();
            #endregion



            var connectionString = builder.Configuration.GetConnectionString("Default");


            // Add services to the container.
            builder.Services.AddOptions();
            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
            builder.Services.AddDbContext<AuthContext>(x => x.UseNpgsql(connectionString));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();


            using (var scope = app.Services.CreateScope())
            {
                var authContext =scope.ServiceProvider.GetRequiredService<AuthContext>();
                authContext.Database.Migrate();
            }

            if (builder.Environment.IsProduction())
                app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}
