using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ScheduledMeets.Business.OAuth;

static class OAuthRegistrations
{
    public static IServiceCollection AddOAuth(this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        services
            .AddScoped<ITokenValidator, JsonWebTokenValidator>();

        services.AddOptions<JsonWebTokenValidatorSettings>()
           .Bind(configuration.GetSection(JsonWebTokenValidatorSettings.Section))
           .ValidateDataAnnotations();

        return services;
    }
}
