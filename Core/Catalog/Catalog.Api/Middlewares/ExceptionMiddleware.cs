using Catalog.Api.Exceptions;
using System.Text.Json;

namespace Catalog.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
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
