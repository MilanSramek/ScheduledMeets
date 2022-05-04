using Dawn;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using ScheduledMeets.Business.UseCases.CreateUserByClaimsPrincipal;
using ScheduledMeets.Business.UseCases.DecodeJsonWebBearerToken;
using ScheduledMeets.Business.UseCases.GetOrCreateUserByBearerToken;
using ScheduledMeets.Business.UseCases.GetUserByClaimsPrincipal;
using ScheduledMeets.Core;

using System.Security.Claims;

namespace ScheduledMeets.Business.UseCases
{
    static class UseCaseRegistrations
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services) =>
            Guard.Argument(services).NotNull().Value
                .AddScoped<IRequestHandler<GetOrCreateUserByBearerTokenRequest, User>, UserConstitutor>()
                .AddScoped<IRequestHandler<CreateUserByClaimsPrincipalRequest, User>, UserCreator>()
                .AddScoped<IRequestHandler<GetUserByClaimsPrincipalRequest, User?>, UserProvider>()
                .AddScoped<IRequestHandler<DecodeJsonWebBearerTokenRequest, ClaimsPrincipal>, JsonWebBearerTokenHandler>();
    }
}
