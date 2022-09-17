using Microsoft.AspNetCore.Http;

using System.Security.Claims;

namespace ScheduledMeets.Internals.Authorization
{
    class AddDeveloperIdentityMiddleware
    {
        private readonly RequestDelegate _next;

        public AddDeveloperIdentityMiddleware(RequestDelegate next) =>
            _next = next ?? throw new ArgumentNullException(nameof(next));

        public async Task InvokeAsync(HttpContext context)
        {
            ClaimsIdentity userIdentity = new(ClaimTypes.Sid);
            userIdentity.AddClaim(new(ClaimTypes.Sid, "developer"));

            context.User = new(userIdentity);

            await _next(context);
        }
    }
}
