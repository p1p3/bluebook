using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Books.Infrastructure.Database
{
    internal static class BooksManagmentContextSeed
    {
        internal static async Task SeedAsync(BooksManagmentContext context,  ILogger<BooksManagmentContext> logger)
        {
            var policy = CreatePolicy(logger, nameof(BooksManagmentContext));

            await policy.ExecuteAsync(async () =>
            {
                using (context)
                {
                    context.Database.Migrate();
                    await context.SaveChangesAsync();
                }
            });
        }

        private static Policy CreatePolicy(ILogger<BooksManagmentContext> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogTrace($"[{prefix}] Exception {exception.GetType().Name} with message ${exception.Message} detected on attempt {retry} of {retries}");
                    }
                );
        }
    }
}
