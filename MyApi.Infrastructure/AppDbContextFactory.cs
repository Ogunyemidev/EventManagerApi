using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using MyApi.Infrastructure.Persistence;

namespace MyApi.Infrastructure
{
    public class AppDbContextFactory
    {
         public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=StudentManagementDb;Username=postgres;Password=;"
            );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}