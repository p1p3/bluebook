using Identity.Core.Infrastructure.Database.Models;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Core.Infrastructure.Mappers
{
    internal static class ClientsConfigurationToIdentityClient
    {
        internal static Client MapToIdentityClient(this ClientConfiguration clientConfiguration)
        {
            return new Client()
            {
                ClientId = clientConfiguration.Id,
                ClientName = clientConfiguration.Name,
                //ClientSecrets =
                //    {
                //    new Secret("secret".Sha256())
                //},
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,
                RedirectUris = clientConfiguration.RedirectUris,
                RequireConsent = false,
                PostLogoutRedirectUris = clientConfiguration.PostLogoutRedirectUris,
                AllowedCorsOrigins = clientConfiguration.AllowedCorsOrigins,
                AllowedScopes = clientConfiguration.AllowedScopes,
            };
        }
    }
}
