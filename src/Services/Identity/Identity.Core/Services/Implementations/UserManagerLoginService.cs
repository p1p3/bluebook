using Identity.Core.Models;
using Identity.Core.Models.Exceptions;
using Identity.Core.Services.Abstractions;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Identity.Core.Services.Implementations
{
    internal class UserManagerLoginService : ILoginService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;

        public UserManagerLoginService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService interaction)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _interaction = interaction;
        }


        public async Task<ApplicationUser> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var validCredenitials = await _userManager.CheckPasswordAsync(user, password);

            if (!validCredenitials) throw new InvalidCredentialsException();

            await _signInManager.SignInAsync(user, true);

            return user;
        }

    }
}
