using System.Collections.Generic;

namespace Identity.Core.Infrastructure.Database.Models
{
    public class ClientConfiguration
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<string> RedirectUris { get; set; }
        public ICollection<string> PostLogoutRedirectUris { get; set; }
        public ICollection<string> AllowedCorsOrigins { get; set; }
        internal ICollection<string> AllowedScopes { get; set; }
    }
}
