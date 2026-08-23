// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// using Microsoft.EntityFrameworkCore;
// using MyApi.Infrastructure.Persistence;

// namespace MyApi.Infrastructure
// {
//     public class AppDbContextFactory
//     {
//          public AppDbContext CreateDbContext(string[] args)
//         {
//             var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

//             optionsBuilder.UseNpgsql(
//                 "Host=localhost;Port=5432;Database=StudentManagementDb;Username=postgres;Password=;"
//             );

//             return new AppDbContext(optionsBuilder.Options);
//         }
//     }
// }

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MyApi.Infrastructure.Persistence;

namespace MyApi.Infrastructure
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../MyApi.API"))
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseNpgsql(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}