
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using Yarp.ReverseProxy.Transforms;
using Yarp.ReverseProxy.Transforms.Builder;

namespace Gateway.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //for https certificates, configs are in appsettings
            builder.WebHost.ConfigureKestrel(options => { });


            #region YARP CONFIG
            builder.Services.AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
                .AddTransforms((transformBuilder) =>
                {
                    transformBuilder.AddRequestTransform(async transformContext =>
                    {

                        var user = transformContext.HttpContext.User;

                        Console.WriteLine($"Is user authenticated:{user.Identity?.IsAuthenticated}");

                        if (user.Identity?.IsAuthenticated == true)
                        {
                            var userId = user.FindFirst("UserId")?.Value;
                            Console.WriteLine($"userId={userId}");
                            if (userId != null)
                                transformContext.ProxyRequest.Headers.Add("X-User-Id", userId);

                            var roles = user.FindAll(ClaimTypes.Role).Select(r => r.Value);

                            if (roles.Any()) 
                                transformContext.ProxyRequest.Headers.Add("X-User-Roles", string.Join(",", roles));
                        }
                        await ValueTask.CompletedTask;
                    });
                });
                 #endregion



            #region JWT CONFIG
            string publicKeyPem;
            if (builder.Environment.IsDevelopment())
            {
                var dir = Directory.GetCurrentDirectory();
                var filePath = Path.Combine(dir,"Infrastructure", "Dev", "public.pem");
                if (File.Exists(filePath) == false)
                    throw new ArgumentException($"Public key file not found: ./{filePath.ToString()}," +
                        "generate it by running auth api for the first time, then copy the public.pem file from a similiar path in " +
                        "auth api folder, to the path written above");

                publicKeyPem = File.ReadAllText(filePath);
            }
            else
            {
                publicKeyPem = Environment.GetEnvironmentVariable("JwtPublicKey") ?? throw new ArgumentException(
                    "Security key must be assigned in environment variables for production environment");
            }
            var jwtOptions = builder.Configuration.GetSection("JwtOptions");
            var rsa = RSA.Create();
            rsa.ImportFromPem(publicKeyPem);
            RsaSecurityKey rsaKey = new(rsa);
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ClockSkew = TimeSpan.FromMinutes(Int32.Parse(jwtOptions["Clockskew"])),
                    ValidateLifetime = true,

                    IssuerSigningKey = rsaKey,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtOptions["Issuer"],
                    ValidateIssuer = true,

                    ValidAudience = jwtOptions["Audience"],
                    ValidateAudience = true

                };
            });

            #endregion

             var app = builder.Build();


            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapReverseProxy();
            app.Run();
        }
    }
}
