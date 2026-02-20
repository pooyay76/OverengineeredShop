using Common.Application.Helpers;
using Common.Infrastructure;
using Feedback.Api.Data;
using Feedback.Api.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PostgreSQL");



// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
{
    foreach (var converter in EventSerializer.Options.Converters)
    {
        x.JsonSerializerOptions.Converters.Add(converter);
    }
});

CommonServiceRegistry.RegisterCommonServices(builder.Services, connectionString);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FeedbackContext>(x => x.UseNpgsql(connectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
