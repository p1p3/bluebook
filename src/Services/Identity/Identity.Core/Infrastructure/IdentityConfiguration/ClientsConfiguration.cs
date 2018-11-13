using Identity.Core.Infrastructure.Database.Models;
using Identity.Core.Infrastructure.Mappers;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Core.Infrastructure.IdentityConfiguration
{
    internal class ClientsConfiguration : IIdentityConfiguration
    {
        private readonly IEnumerable<ClientConfiguration> _clientsConfiguration;

        public ClientsConfiguration(IEnumerable<ClientConfiguration> clientsConfiguration)
        {
            _clientsConfiguration = clientsConfiguration;
        }

        public async Task Configure(ConfigurationDbContext context)
        {
           
            if (!context.Clients.Any())
            {
                foreach (var client in _clientsConfiguration)
                {
                    client.AllowedScopes = AllowedScopes;
                    var identityClient = client.MapToIdentityClient();

                    context.Clients.Add(identityClient.ToEntity());
                }
                await context.SaveChangesAsync();
            }
        }

        private static ICollection<string> AllowedScopes =>
             new List<string>
                {
                   IdentityServerConstants.StandardScopes.OpenId,
                   IdentityServerConstants.StandardScopes.Profile,
                   Resources.Books,
                };
    }
}
