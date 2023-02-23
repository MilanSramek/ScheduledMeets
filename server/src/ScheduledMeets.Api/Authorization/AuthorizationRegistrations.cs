using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

using static ScheduledMeets.Api.Authorization.PolicyConsts;

namespace ScheduledMeets.Api.Authorization;

internal static class AuthorizationRegistrations
{
    public static IServiceCollection AddAuthor(this IServiceCollection services) => services
        .AddAuthorization(options =>
        {
            options
                .AddPolicy(BeTheUser, _ => _.Requirements.Add(new BeTheUserRequirement()));
        })
       .AddSingleton<IAuthorizationHandler, BeTheUserHandler>();
}
