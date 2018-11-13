using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using Books.Infrastructure.Database.Extensions;
using Books.Infrastructure.Database;
using Books.Core.Repositories;
using Books.Infrastructure.Database.Repositories;

namespace Books.Infrastructure
{
    public static partial class Initializer
    {
        public static void InitializeStorages(IServiceProvider serviceProvider)
        {
            serviceProvider
           .MigrateDbContext<BooksManagmentContext>((context, services) =>
           {
               var logger = services.GetService<ILogger<BooksManagmentContext>>();
               BooksManagmentContextSeed.SeedAsync(context, logger).Wait();
           });
        }


        public static void InitializeServices(IServiceCollection services, string sqlDatabaseConnectionString)
        {

            var migrationsAssemblyName = typeof(Initializer).GetTypeInfo().Assembly.GetName().Name;

            // Add framework services.
            services.AddDbContext<BooksManagmentContext>(options =>
             options.UseSqlServer(sqlDatabaseConnectionString,
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(migrationsAssemblyName);
                                         sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                     }));

       
            services.AddScoped<IBookRepository, BookRepository>();

        }

    }
}

