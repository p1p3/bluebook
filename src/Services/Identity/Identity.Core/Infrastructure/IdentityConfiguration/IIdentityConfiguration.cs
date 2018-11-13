using IdentityServer4.EntityFramework.DbContexts;
using System.Threading.Tasks;

namespace Identity.Core.Infrastructure.IdentityConfiguration
{
    public interface IIdentityConfiguration
    {
        Task Configure(ConfigurationDbContext context);
    }
}
