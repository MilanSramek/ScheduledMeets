using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;

using Microsoft.Extensions.DependencyInjection;

namespace ScheduledMeets.Persistance.Model;

internal static class MappingConfiguration
{
    /// <summary>
    /// Adds all mapping profiles.
    /// </summary>
    /// <param name="config"></param>
    public static IMapperConfigurationExpression AddProfiles(
        this IMapperConfigurationExpression config)
    {
        ArgumentNullException.ThrowIfNull(config);
        config.AddProfile<UserProfile>();

        return config;
    }

    public static IMapperConfigurationExpression AddConfigurations(
        this IMapperConfigurationExpression config)
    {
        ArgumentNullException.ThrowIfNull(config);
        return config.AddExpressionMapping();
    }

    public static IServiceCollection AddModel(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        return services.AddAutoMapper(config => config
            .AddConfigurations()
            .AddProfiles());
    }
}
