using Microsoft.EntityFrameworkCore;
using MyApi.Infrastructure.Persistence;
using MyApi.Infrastructure.Persistence.Repositories;
using MyApi.Application.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();