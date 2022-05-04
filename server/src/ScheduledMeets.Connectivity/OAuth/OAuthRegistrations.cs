using Dawn;

using IdentityModel.Client;

using Microsoft.Extensions.DependencyInjection;

using ScheduledMeets.Business.Interfaces;

namespace ScheduledMeets.Connectivity.OAuth;

static class OAuthRegistrations
{
    public static IServiceCollection AddOAuth(this IServiceCollection services) =>
        Guard.Argument(services).NotNull().Value
            .AddSingleton<IProvider<DiscoveryDocumentResponse>, DiscoveryDocumentProvider>()
            .AddSingleton<IProvider<JsonWebKeySetResponse>, JsonWebKeySetProvider>();
}
