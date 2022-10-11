using IdentityModel.Client;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ScheduledMeets.Business.Interfaces;

namespace ScheduledMeets.Connectivity.OAuth;

static class OAuthRegistrations
{
    public static IServiceCollection AddOAuth(this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        services
            .AddSingleton<IProvider<DiscoveryDocumentResponse>, DiscoveryDocumentProvider>()
            .AddSingleton<IProvider<JsonWebKeySetResponse>, JsonWebKeySetProvider>();

        services.AddOptions<DiscoveryDocumentProviderSettings>()
            .Bind(configuration.GetRequiredSection(DiscoveryDocumentProviderSettings.Section))
            .ValidateDataAnnotations();
        services.AddOptions<JsonWebKeySetProviderSettings>()
           .Bind(configuration.GetSection(JsonWebKeySetProviderSettings.Section))
           .ValidateDataAnnotations();

        return services;
    }
}
