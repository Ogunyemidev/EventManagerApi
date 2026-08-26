using MyApi.API.MiddleWare;
using MyApi.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    EnvironmentName = Environment.GetEnvironmentVariable(
        "ASPNETCORE_ENVIRONMENT")
});

// Infrastructure
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddDependencyInjection(builder.Configuration);


// Controllers
builder.Services.AddControllers();


// OpenAPI
builder.Services.AddOpenApi();

// Swagger
builder.Services.AddSwaggerGen();

// Authorization
builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.MapOpenApi();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Global exception handling
app.UseExceptionHandling();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();