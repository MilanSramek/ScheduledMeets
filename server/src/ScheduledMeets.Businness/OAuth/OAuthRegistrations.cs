using Dawn;

using Microsoft.Extensions.DependencyInjection;

namespace ScheduledMeets.Business.OAuth;

static class OAuthRegistrations
{
    public static IServiceCollection AddOAuth(this IServiceCollection services) =>
        Guard.Argument(services).NotNull().Value
            .AddScoped<ITokenValidator, JsonWebTokenValidator>();
}
