using Microsoft.EntityFrameworkCore;
using MyApi.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
});

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Controllers
builder.Services.AddControllers();

// OpenAPI
builder.Services.AddOpenApi();

// Swagger
builder.Services.AddSwaggerGen();

var app = builder.Build();

// OpenAPI document
app.MapOpenApi();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.MapControllers();

app.Run();