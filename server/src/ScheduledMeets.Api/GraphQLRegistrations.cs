using Microsoft.Extensions.DependencyInjection;

using ScheduledMeets.Api.Authentication;
using ScheduledMeets.Api.Authorization;
using ScheduledMeets.Api.DataLoaders;

namespace ScheduledMeets.Api;

public static class GraphQLRegistrations
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services
            .AddAuthor()
            .AddGraphQL();

        return services;
    }

    private static IServiceCollection AddGraphQL(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddAuthorization()
            .AddGlobalObjectIdentification()
            .AddMutationConventions()
            .AddQueryFieldToMutationPayloads()
            .AddProjections()
            .AddDiagnosticEventListener<ExecutionErrorLogger>()
            .AddHttpRequestInterceptor<UserIdentityInterceptor>()
            .AddDataLoaders()
            .AddApiTypes();

        return services;
    }
}
