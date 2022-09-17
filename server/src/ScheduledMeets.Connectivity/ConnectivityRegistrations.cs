using Microsoft.Extensions.DependencyInjection;

using ScheduledMeets.Connectivity.OAuth;
using ScheduledMeets.Connectivity.Tools;

namespace ScheduledMeets.Connectivity;

public static class ConnectivityRegistrations
{
    public static IServiceCollection AddConnectivity(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        return services
            .AddOAuth()
            .AddTools();
    }
}
