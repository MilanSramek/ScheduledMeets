using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace ScheduledMeets;

static class ServicesRegistrations
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        return services
            .AddHttpClient()
            .AddMediator();
    }

    private static IServiceCollection AddMediator(this IServiceCollection services) => services
        .AddScoped<ISender, Mediator>()
        .AddScoped<ServiceFactory>(context =>
        {
            return type => context.GetRequiredService(type);
        });
}
