using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApi.Application.Authentication;
using MyApi.Application.IRepositories;
using MyApi.Application.Services.Implementations;
using MyApi.Application.Services.Interfaces;
using MyApi.Infrastructure.Persistence;
using MyApi.Infrastructure.Persistence.Repositories;
using MyApi.Application.Storage;
using MyApi.Infrastructure.Storage;

namespace MyApi.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDependencyInjection(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Application services
            services.AddScoped<IUserServices, UserService>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();

            // Authentication
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ICurrentUser, CurrentUser>();

            // JWT settings
            services.Configure<JwtSettings>(
                configuration.GetSection("JwtSettings"));

            // File storage
            services.AddScoped<AwsS3FileStorage>();
            services.AddScoped<AzureBlobFileStorage>();
            services.AddScoped<FileStorageFactory>();
            services.AddScoped<LocalFileStorage>();

            services.AddScoped<IFileStorage>(sp =>
                sp.GetRequiredService<FileStorageFactory>().Create());

            return services;
        }

        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString =
                configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' not found.");

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));

            return services;
        }
    }
}