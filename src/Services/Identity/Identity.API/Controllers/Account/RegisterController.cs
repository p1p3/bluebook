using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.API.Models.AccountViewModels;
using Identity.Core.Models;
using Identity.Core.Models.Exceptions;
using Identity.Core.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers.Account
{
    [Route("Account/Register")]
    public class RegisterController : Controller
    {

        private readonly IRegisterService _registerService;
        private static readonly string ViewUri = "~/Views/Account/Register.cshtml";

        public RegisterController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null, string culture = "es")
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(ViewUri);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid) return View(ViewUri, model);

            //TODO : Mapper
            var user = new RegistrationData
            {
                Email = model.Email,
                Country = model.Country,
                LastName = model.LastName,
                Name = model.Name,
                Password = model.Password
            };

            //TODO : Put into a middleware/filter
            try
            {
                await _registerService.Register(user);
            }
            catch (RegistrationGeneralException e)
            {
                //TODO : Use localization and only use the code
                AddErrors(e.InnerExceptions);
                // If we got this far, something failed, redisplay form
                return View(ViewUri, model);
                throw;
            }


            if (returnUrl != null)
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                    return Redirect(returnUrl);
                else if (ModelState.IsValid)
                    return RedirectToAction("login", "account", new { returnUrl = returnUrl });
            }

            return RedirectToAction("index", "home");
        }

        private void AddErrors(IEnumerable<Exception> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(string.Empty, error.Message);
            }
        }
    }
}