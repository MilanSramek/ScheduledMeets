using Microsoft.Extensions.DependencyInjection;

namespace ScheduledMeets.Connectivity.Tools;

static class ToolsRegistrations
{
    public static IServiceCollection AddTools(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        return services
            .AddSingleton<ITimeProvider, TimeProvider>();
    }
}
