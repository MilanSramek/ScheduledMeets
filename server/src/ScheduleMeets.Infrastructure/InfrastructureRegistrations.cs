using Microsoft.Extensions.DependencyInjection;

using ScheduledMeets.Business.Interfaces;

namespace ScheduledMeets.Infrastructure;

public static class InfrastructureRegistrations
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));

        return services
            .AddScoped(typeof(IProcessor<>), typeof(VoidRequestProcessor<>))
            .AddScoped(typeof(IProcessor<,>), typeof(ResponseRequestProcessor<,>));
    }
}
