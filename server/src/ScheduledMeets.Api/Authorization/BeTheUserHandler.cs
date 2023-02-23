using HotChocolate.Resolvers;

using Microsoft.AspNetCore.Authorization;

using ScheduledMeets.View;

using System.Security.Principal;

using static ScheduledMeets.Api.Authentication.AuthenticationGlobalState;

namespace ScheduledMeets.Api.Authorization;

internal class BeTheUserHandler :
    AuthorizationHandler<BeTheUserRequirement, IMiddlewareContext>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        BeTheUserRequirement requirement, 
        IMiddlewareContext resource)
    {
        if (resource.Result is not UserView user)
            return Task.CompletedTask;

        IIdentity? userIdentity = resource.GetGlobalState<IIdentity>(User);
        if (userIdentity is not { IsAuthenticated: true, Name: string identityName })
            return Task.CompletedTask;

        if (identityName == user.Username)
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
