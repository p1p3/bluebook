using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace Identity.Core.Infrastructure.IdentityConfiguration
{
    internal class ApiResourcesConfiguration : IIdentityConfiguration
    {
        public async Task Configure(ConfigurationDbContext context)
        {
            if (!context.ApiResources.Any())
            {
                foreach (var api in Apis)
                {
                    context.ApiResources.Add(api.ToEntity());
                }

                await context.SaveChangesAsync();
            }
        }

        // ApiResources define the apis in your system
        private static IEnumerable<ApiResource> Apis =>
             new List<ApiResource>
            {
                new ApiResource(Resources.Books, "Books Service")
            };

    }
}
