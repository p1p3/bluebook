using System.Threading.Tasks;

namespace Identity.Core.Services.Abstractions
{
    public interface ILogoutService
    {
        Task Logout(string logoutId);
    }
}
