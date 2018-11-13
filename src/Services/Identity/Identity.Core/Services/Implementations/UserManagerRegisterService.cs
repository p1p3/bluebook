using System.Linq;
using System.Threading.Tasks;
using Identity.Core.Models;
using Identity.Core.Models.Exceptions;
using Identity.Core.Services.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Identity.Core.Services.Implementations
{
    internal class UserManagerRegisterService : IRegisterService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserManagerRegisterService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Register(RegistrationData data)
        {
            var user = new ApplicationUser
            {
                UserName = data.Email,
                Email = data.Email,
                Country = data.Country,
                LastName = data.LastName,
                Name = data.Name,
            };

            var result = await _userManager.CreateAsync(user, data.Password);

            if (!result.Succeeded || result.Errors.Any()) throw new RegistrationGeneralException(result);
        }
    }
}
