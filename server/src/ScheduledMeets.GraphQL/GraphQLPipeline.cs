using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;


namespace ScheduledMeets.GraphQL
{
    public static class GraphQLPipeline
    {
        public static TApplicationBuilder UseGraphQL<TApplicationBuilder>(
            this TApplicationBuilder builder)
            where TApplicationBuilder : IApplicationBuilder, IEndpointRouteBuilder
        {
            builder
                .UseAuthentication()
                .UseAuthorization();
            builder
                .MapGraphQL();

            return builder;
        }
    }
}
