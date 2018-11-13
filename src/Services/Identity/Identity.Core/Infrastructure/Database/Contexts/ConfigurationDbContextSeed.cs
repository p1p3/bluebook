using Identity.Core.Infrastructure.IdentityConfiguration;
using Identity.Core.Infrastructure.Database.Models;
using IdentityServer4.EntityFramework.DbContexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Core.Infrastructure.Database.Contexts
{
    internal class ConfigurationDbContextSeed
    {
        private readonly IEnumerable<IIdentityConfiguration> _configurations;

        internal ConfigurationDbContextSeed(IEnumerable<ClientConfiguration> clientsConfiguration)
        {
            _configurations = new List<IIdentityConfiguration>
            {
                new ClientsConfiguration(clientsConfiguration),
                new IdentityResourcesConfiguration(),
                new ApiResourcesConfiguration(),
            };
        }

        internal async Task SeedAsync(ConfigurationDbContext context)
        {
            foreach (var configuration in _configurations)
            {
                await configuration.Configure(context);
            }
        }

    }
}
