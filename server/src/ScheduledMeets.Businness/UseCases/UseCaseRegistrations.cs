using MediatR;

using Microsoft.Extensions.DependencyInjection;

using ScheduledMeets.Business.UseCases.CreateUserByClaimsPrincipal;
using ScheduledMeets.Business.UseCases.GetOrCreateUserByBearerToken;
using ScheduledMeets.Core;

namespace ScheduledMeets.Business.UseCases;

static class UseCaseRegistrations
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        return services
            .AddScoped<IRequestHandler<GetOrCreateUserByBearerTokenRequest, User>, UserConstitutor>()
            .AddScoped<IRequestHandler<CreateUserByClaimsPrincipalRequest, User>, UserCreator>()
            .AddScoped<IUserProvider, UserProvider>();
    }
}
