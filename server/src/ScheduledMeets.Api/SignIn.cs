using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using ScheduledMeets.Api.Authentication;
using ScheduledMeets.Business.Interfaces;
using ScheduledMeets.Business.UseCases.GetOrCreateUserByBearerToken;
using ScheduledMeets.View;

using System.Security.Claims;

namespace ScheduledMeets.Api;

[MutationType]
internal static class SignIn
{
    public static async Task<UserView> SignInAsync(
        string idToken,
        [GlobalState]HttpContext httpContext,
        [Service]IProcessor<GetOrCreateUserByBearerTokenRequest, Core.User> userProcessor,
        [Service]IWithIdReader<UserView> userReader,
        CancellationToken cancellationToken)
    {
        Core.User user = await userProcessor.ProcessAsync(
            new GetOrCreateUserByBearerTokenRequest(idToken),
            cancellationToken);

        ClaimsIdentity userIdentity = new(AuthenticationType.Federation);
        userIdentity.AddClaim(new(ClaimTypes.Name, user.Username));

        ClaimsPrincipal userPrincipal = new(userIdentity);
        await httpContext.SignInAsync(userPrincipal);

        return await userReader.ReadAsync(user.Id, cancellationToken);
    }
}