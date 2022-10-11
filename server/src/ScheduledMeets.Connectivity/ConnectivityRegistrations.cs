using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ScheduledMeets.Connectivity.OAuth;
using ScheduledMeets.Connectivity.Tools;

namespace ScheduledMeets.Connectivity;

public static class ConnectivityRegistrations
{
    public static IServiceCollection AddConnectivity(this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        return services
            .AddOAuth(configuration)
            .AddTools();
    }
}
