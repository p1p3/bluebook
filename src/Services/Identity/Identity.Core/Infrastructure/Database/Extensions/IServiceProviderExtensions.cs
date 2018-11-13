using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using System.Data.SqlClient;

namespace Identity.Core.Infrastructure.Database.Extensions
{
    public static class IServiceProviderExtensions
    {

        private static readonly RetryPolicy _retryPolicy = Policy.Handle<SqlException>()
                    .WaitAndRetry(new TimeSpan[]
                    {
                             TimeSpan.FromSeconds(3),
                             TimeSpan.FromSeconds(5),
                             TimeSpan.FromSeconds(8),
                    });


        public static IServiceProvider MigrateDbContext<TContext>(this IServiceProvider services, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {
            var logger = services.GetRequiredService<ILogger<TContext>>();
            var context = services.GetService<TContext>();

            try
            {
                logger.LogInformation($"Migrating database associated with context {typeof(TContext).Name}");

                _retryPolicy.Execute(() =>
                {
                    context.Database.Migrate();
                    seeder(context, services);
                });


                logger.LogInformation($"Migrated database associated with context {typeof(TContext).Name}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while migrating the database used on context {typeof(TContext).Name}");
            }

            return services;
        }

    }
}


