using HotChocolate.AspNetCore.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;

namespace ScheduledMeets.GraphQL;

public static class GraphQLPipeline
{
    public static TApplicationBuilder UseGraphQL<TApplicationBuilder>(
        this TApplicationBuilder builder,
        IHostEnvironment environment)
        where TApplicationBuilder : IApplicationBuilder, IEndpointRouteBuilder
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(environment);

        GraphQLEndpointConventionBuilder graphqlBuilder = builder
            .MapGraphQL();

        graphqlBuilder.WithOptions(new()
        {
           EnableSchemaRequests = environment.IsDevelopment()
        });

        return builder;
    }
}
