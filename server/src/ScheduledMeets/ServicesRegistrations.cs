using Dawn;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace ScheduledMeets;

static class ServicesRegistrations
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services) =>
        Guard.Argument(services).NotNull().Value
            .AddHttpClient()
            .AddMediator();

    private static IServiceCollection AddMediator(this IServiceCollection services) => services
        .AddScoped<ISender, Mediator>()
        .AddScoped<ServiceFactory>(context =>
        {
            return type => context.GetRequiredService(type);
        });
}
