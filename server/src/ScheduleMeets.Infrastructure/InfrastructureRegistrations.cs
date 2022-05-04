using Dawn;

using Microsoft.Extensions.DependencyInjection;

using ScheduledMeets.Business.Interfaces;

namespace ScheduledMeets.Infrastructure
{
    public static class InfrastructureRegistrations
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
            Guard.Argument(services, nameof(services)).NotNull().Value
                .AddScoped(typeof(IProcessor<>), typeof(VoidRequestProcessor<>))
                .AddScoped(typeof(IProcessor<,>), typeof(ResponseRequestProcessor<,>));
    }
}
