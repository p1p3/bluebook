using Identity.Core.Services.Abstractions;
using IdentityServer4.Services;
using System.Threading.Tasks;

namespace Identity.Core.Services.Implementations
{
    internal class IdentityInteractionsExternalLoginService : IExternalLoginService
    {
        private readonly IIdentityServerInteractionService _interaction;

        public IdentityInteractionsExternalLoginService(IIdentityServerInteractionService interaction)
        {
            _interaction = interaction;
        }

    
        public async Task<ExternalLoginValidationResult> IsExternalLogin(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            if (context?.IdP == null) return ExternalLoginValidationResult.NonExternalLogin();

            return ExternalLoginValidationResult.ExternalLogin(context.ClientId);
        }

        public Task<bool> IsValidExternalReturnUrl(string returnUrl)
        {
            return Task.FromResult(_interaction.IsValidReturnUrl(returnUrl));
        }
    }
}
