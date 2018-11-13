using Identity.Core.Infrastructure.Database.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Identity.Core.Infrastructure.Database.Contexts
{
    internal class ApplicationDbContextSeed
    {
        //private readonly IPasswordHasher<ApplicationUser> _passwordHasher = new PasswordHasher<ApplicationUser>();

        public Task SeedAsync(ApplicationDbContext context, IHostingEnvironment env,
            ILogger<ApplicationDbContextSeed> logger, ApplicationDbContextSettings settings)
        {
            return Task.CompletedTask;
        }


    }
}
