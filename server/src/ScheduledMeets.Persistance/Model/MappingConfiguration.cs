using AutoMapper;

using Microsoft.Extensions.DependencyInjection;

namespace ScheduledMeets.Persistance.Model;

internal static class MappingConfiguration
{
    public static void AddProfiles(IMapperConfigurationExpression config)
    {
        ArgumentNullException.ThrowIfNull(config);
        config.AddProfile<UserProfile>();
    }

    public static IServiceCollection AddModel(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        return services.AddAutoMapper(AddProfiles);
    }
}
