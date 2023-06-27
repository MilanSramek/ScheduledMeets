using Microsoft.Extensions.DependencyInjection;

namespace ScheduledMeets.View;

public static class ViewRegistrations
{
    public static IServiceCollection AddView(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        return services
            .AddScoped<IMemberExtender, MemberExtender>();
    }
}