using Identity.Core.Models;
using Identity.Core.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Identity.Core.Services.Implementations
{
    internal class IdentityServiceLogoutService : ILogoutService
    {

        SignInManager<ApplicationUser> _signInManager;

        public IdentityServiceLogoutService(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }


        public async Task Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
        }
    }
}
