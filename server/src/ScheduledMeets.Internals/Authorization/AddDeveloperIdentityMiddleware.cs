using Microsoft.AspNetCore.Http;

using System.Security.Claims;

namespace ScheduledMeets.Internals.Authorization;

internal class AddDeveloperIdentityMiddleware
{
    private readonly RequestDelegate _next;

    public AddDeveloperIdentityMiddleware(RequestDelegate next) =>
        _next = next ?? throw new ArgumentNullException(nameof(next));

    public async Task InvokeAsync(HttpContext context)
    {
        ClaimsIdentity userIdentity = new(ClaimTypes.Name);
        userIdentity.AddClaim(new(ClaimTypes.Name, "developer"));

        context.User = new(userIdentity);

        await _next(context);
    }
}
