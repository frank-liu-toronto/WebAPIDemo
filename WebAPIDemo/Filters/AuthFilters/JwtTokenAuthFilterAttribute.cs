using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPIDemo.Authority;

namespace WebAPIDemo.Filters.AuthFilters
{
    public class JwtTokenAuthFilterAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();

            if (!Authenticator.VerifyToken(token, configuration.GetValue<string>("SecretKey")))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
