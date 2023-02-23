using HotChocolate.Execution.Configuration;

using Microsoft.Extensions.DependencyInjection.Extensions;

using ScheduledMeets.View;

namespace ScheduledMeets.Api.DataLoaders;

internal static class DataLoaderRegistrations
{
    public static IRequestExecutorBuilder AddDataLoaders(this IRequestExecutorBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        return builder
            .AddDataLoader(typeof(IWithIdReader<>), typeof(WithIdLoader<>));
    }

    private static IRequestExecutorBuilder AddDataLoader(
        this IRequestExecutorBuilder builder,
        Type serviceType,
        Type implementationType)
        {
            builder.Services
                .TryAddScoped(serviceType, implementationType);
            return builder;
        }
}