using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

using ScheduledMeets.Api.Authentication;
using ScheduledMeets.Business.Interfaces;
using ScheduledMeets.Business.UseCases.GetOrCreateUserByBearerToken;
using ScheduledMeets.Core;
using ScheduledMeets.View;

using System.Security.Claims;

namespace ScheduledMeets.Api;

[MutationType]
internal static class SignIn
{
    public static async Task<UserView> SignInAsync(
        string idToken,
        [Service]IHttpContextAccessor httpContextAccessor,
        [Service]IProcessor<GetOrCreateUserByBearerTokenRequest, User> userProcessor,
        [Service]IWithIdReader<UserView> userReader,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(httpContextAccessor);
        ArgumentNullException.ThrowIfNull(httpContextAccessor.HttpContext);
        ArgumentNullException.ThrowIfNull(userProcessor);
        ArgumentNullException.ThrowIfNull(userReader);

        User user = await userProcessor.ProcessAsync(new(idToken), cancellationToken);

        ClaimsIdentity userIdentity = new(AuthenticationType.Federation);
        userIdentity.AddClaim(new(ClaimTypes.Name, user.Username));

        ClaimsPrincipal userPrincipal = new(userIdentity);
        await httpContextAccessor.HttpContext.SignInAsync(userPrincipal);

        return await userReader.ReadAsync(user.Id, cancellationToken);
    }
}