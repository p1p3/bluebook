using Identity.API.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Identity.API.Extensions
{
    public static class RequestCultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestCulture(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestCultureMiddleware>();
        }
    }
}
