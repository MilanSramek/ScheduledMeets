using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ScheduledMeets.Business.OAuth;
using ScheduledMeets.Business.RequestPipeline;
using ScheduledMeets.Business.UseCases;

namespace ScheduledMeets.Business;

public static class BusinessRegistrations
{
    public static IServiceCollection AddBusiness(this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        return services
            .AddUseCases()
            .AddOAuth(configuration)
            .AddRequestPipeline();
    }
}
