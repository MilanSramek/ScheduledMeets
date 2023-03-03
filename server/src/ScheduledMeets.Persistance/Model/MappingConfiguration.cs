using Microsoft.Extensions.DependencyInjection;

namespace ScheduledMeets.Persistance.Model;

internal static class MappingConfiguration
{
    public static IServiceCollection AddModel(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        return services.AddAutoMapper(config =>
        {
            config.AddProfile<UserProfile>();
        });
    }
}
