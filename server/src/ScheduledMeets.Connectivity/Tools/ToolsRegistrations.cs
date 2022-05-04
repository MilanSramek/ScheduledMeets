using Dawn;

using Microsoft.Extensions.DependencyInjection;

namespace ScheduledMeets.Connectivity.Tools;

static class ToolsRegistrations
{
    public static IServiceCollection AddTools(this IServiceCollection services) =>
        Guard.Argument(services).NotNull().Value
            .AddSingleton<ITimeProvider, TimeProvider>();
}
