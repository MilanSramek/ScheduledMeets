using Microsoft.Extensions.DependencyInjection;

namespace ScheduledMeets.Business.OAuth;

static class OAuthRegistrations
{
    public static IServiceCollection AddOAuth(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        return services
            .AddScoped<ITokenValidator, JsonWebTokenValidator>();
    }
}
