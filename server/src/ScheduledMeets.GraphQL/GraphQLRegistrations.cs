using Microsoft.Extensions.DependencyInjection;

using ScheduledMeets.Internals.GraphQL;

namespace ScheduledMeets.GraphQL;

public static class GraphQLRegistrations
{
    public static IServiceCollection AddGraphQL(this IServiceCollection services)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));

        services
            .AddGraphQLServer()
            .AddAuthorization()
            .AddQueryType<QueriesType>()
            .AddMutationType<MutationsType>()
            .AddDiagnosticEventListener<ExecutionErrorLogger>();

        return services;
    }
}
