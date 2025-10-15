using Catalog_API.Exceptions;
using System.Text.Json;

namespace Catalog_API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IWebHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            this.next = next;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await next(context);
            }
            catch (Exception ex)
            {

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex is KeyNotFoundException ? 404 : ex is ExceptionBase ? 400 : 500;
                await context.Response.WriteAsync(JsonSerializer.Serialize(ex.Message));

            }

        }
    }
}
