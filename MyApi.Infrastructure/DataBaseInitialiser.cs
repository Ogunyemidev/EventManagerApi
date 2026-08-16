

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyApi.Domain;
using MyApi.Infrastructure.Persistence;

namespace MyApi.Infrastructure
{
    public class DataBaseInitialiser
    {
        public static async Task SeedUserData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<AppDbContext>>();

            try
            {
                logger.LogInformation("Applying database migrations...");
                await context.Database.MigrateAsync();
                logger.LogInformation("Database migrations applied successfully.");

                if (!await context.Users.AnyAsync())
                {
                    var user = new User
                    {
                        FirstName = "Admin",
                        LastName = "User",
                        Email = "admin@yopmail.com",
                      
                    };

                    await context.Users.AddAsync(user);
                    await context.SaveChangesAsync();

                    logger.LogInformation("Default user seeded successfully.");
                }

                logger.LogInformation("Database initialization completed.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error initializing database.");
                throw;
            }
        }
    }
}