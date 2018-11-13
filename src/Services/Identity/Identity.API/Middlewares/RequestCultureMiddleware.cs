using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Middlewares
{
    public class RequestCultureMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestCultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var cultureQuery = context.Request.Query["culture"];
            if (string.IsNullOrEmpty(cultureQuery))
            {
                await _next(context);
                return;
            }

            context.Request.Cookies.TryGetValue(CookieRequestCultureProvider.DefaultCookieName, out string currentCulture);

            var userHasNotSetCulture = string.IsNullOrEmpty(currentCulture);
            if (userHasNotSetCulture)
            {
                currentCulture = cultureQuery;
                SetCultureCookie(currentCulture, context);
            }
            else
            {
                currentCulture = currentCulture.Split("|").Where(param => param.Contains("c=")).First().Split("=").Last();
                if (currentCulture != cultureQuery)
                {
                    currentCulture = cultureQuery;
                    UpdateCultureCookie(cultureQuery, context);
                }
            }


            var culture = new CultureInfo(currentCulture);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;

            await _next(context);
        }

        private void SetCultureCookie(string culture, HttpContext context)
        {
            context.Response.Cookies.Append(
               CookieRequestCultureProvider.DefaultCookieName,
               CookieRequestCultureProvider.MakeCookieValue(
                   new RequestCulture(culture)),
                   new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
        }

        private void UpdateCultureCookie(string culture, HttpContext context)
        {
            context.Response.Cookies.Delete(CookieRequestCultureProvider.DefaultCookieName);
            SetCultureCookie(culture, context);
        }
    }


}
