using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPIDemo.Attributes;
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

            var claims = Authenticator.VerifyToken(token, configuration.GetValue<string>("SecretKey"));

            if (claims == null)
            {
                context.Result = new UnauthorizedResult(); //401
            }
            else
            {
                var requiredClaims = context.ActionDescriptor.EndpointMetadata
                        .OfType<RequiredClaimAttribute>()
                        .ToList();

                // 403
                if (requiredClaims != null && !requiredClaims.All(rc => claims.Any(c => c.Type.ToLower() == rc.ClaimType.ToLower() &&
                                                                                       c.Value.ToLower() == rc.ClaimValue.ToLower())))
                {
                    context.Result = new StatusCodeResult(403);
                }
            }


            
        }
    }
}
