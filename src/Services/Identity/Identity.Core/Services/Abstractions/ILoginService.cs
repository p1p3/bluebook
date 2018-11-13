using Identity.Core.Models;
using System.Threading.Tasks;

namespace Identity.Core.Services.Abstractions
{
    public interface ILoginService
    {
        Task<ApplicationUser> Login(string email, string password);
    }
}
