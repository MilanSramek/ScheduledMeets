using Dawn;

using Microsoft.Extensions.DependencyInjection;

using ScheduledMeets.Business.OAuth;
using ScheduledMeets.Business.UseCases;

namespace ScheduledMeets.Business;

public static class BusinessRegistrations
{
    public static IServiceCollection AddBusiness(this IServiceCollection services) =>
        Guard.Argument(services).NotNull().Value
            .AddUseCases()
            .AddOAuth();
}
