using HotChocolate;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

using ScheduledMeets.Business.Interfaces;
using ScheduledMeets.Business.UseCases.GetOrCreateUserByBearerToken;
using ScheduledMeets.Core;

using System.Security.Claims;

namespace ScheduledMeets.Business.Fasade;

public class SignInResolver
{
    public async Task<User> SignInAsync(
        string idToken,
        [Service] IHttpContextAccessor httpContextAccessor,
        [Service] IProcessor<GetOrCreateUserByBearerTokenRequest, User> userProvider,
        CancellationToken cancellationToken)
    {
        User user = await userProvider.ProcessAsync(
            new GetOrCreateUserByBearerTokenRequest(idToken),
            cancellationToken);

        ClaimsIdentity userIdentity = new();
        userIdentity.AddClaim(new(ClaimTypes.Sid, user.Id.ToString()));

        ClaimsPrincipal userPrincipal = new(userIdentity);

        HttpContext context = httpContextAccessor.HttpContext
            ?? throw new ApplicationException("No context.");
        await context.SignInAsync(userPrincipal);

        return user;
    }
}