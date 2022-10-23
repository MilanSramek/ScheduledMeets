using MediatR;
using MediatR.Pipeline;

using Microsoft.Extensions.DependencyInjection;

namespace ScheduledMeets.Business.RequestPipeline;

internal static class RequestPipelineRegistrations
{
    public static IServiceCollection AddRequestPipeline(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        return services
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>))
            .AddScoped(typeof(IRequestPostProcessor<,>), typeof(SaveChagesPostprocessor<,>));
    }
}
