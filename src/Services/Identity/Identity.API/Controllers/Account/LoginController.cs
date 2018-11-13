using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Identity.API.Models.AccountViewModels;
using Identity.Core.Models.Exceptions;
using Identity.Core.Services.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers.Account
{
    [Route("Account/Login")]
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IExternalLoginService _externalLoginService;
        private static readonly string ViewUri = "~/Views/Account/Login.cshtml";

        public LoginController(ILoginService loginService, IExternalLoginService externalLoginService)
        {
            _loginService = loginService;
            _externalLoginService = externalLoginService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl)
        {
            var externalLoginValidation = await _externalLoginService.IsExternalLogin(returnUrl);
            if (externalLoginValidation.IsExternal) return ExternalLogin(externalLoginValidation.IdP, returnUrl);

            ViewData["ReturnUrl"] = returnUrl;
            return View(ViewUri, new LoginViewModel());
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            ViewData["ReturnUrl"] = model.ReturnUrl;
            if (!ModelState.IsValid) return View(ViewUri, model);

            try
            {
                var user = await _loginService.Login(model.Email, model.Password);

                var isValidReturnUrl = await _externalLoginService.IsValidExternalReturnUrl(model.ReturnUrl);
                if (!isValidReturnUrl) return Redirect("~/");

                return Redirect(model.ReturnUrl);
            }
            catch (InvalidCredentialsException)
            {
                //TODO : GLobalization and error catch middleware
                ModelState.AddModelError("", "Invalid username or password.");
                return View(ViewUri, model);
            }
        }


        /// <summary>
        /// initiate roundtrip to external authentication provider
        /// </summary>
        [HttpGet("/ExternalLogin")]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            if (returnUrl != null)
            {
                returnUrl = UrlEncoder.Default.Encode(returnUrl);
            }
            returnUrl = "/account/externallogincallback?returnUrl=" + returnUrl;

            // start challenge and roundtrip the return URL
            var props = new AuthenticationProperties
            {
                RedirectUri = returnUrl,
                Items = { { "scheme", provider } }
            };
            return new ChallengeResult(provider, props);
        }
    }
}