using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

using ScheduledMeets.Internals.Authentication;

namespace ScheduledMeets.GraphQL;

public static class GraphQLRegistrations
{
    public static IServiceCollection AddGraphQL(this IServiceCollection services)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));

        services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookieInternal(options =>
            {
                options.DisableOnChallangeRedirection();
                options.DisableOnForbiddenRedirection();
                options.Cookie.HttpOnly = true;
            });
        services
            .AddAuthorization();

        services
            .AddGraphQLServer()
            .AddAuthorization()
            .AddQueryType<QueriesType>()
            .AddMutationType<MutationsType>();

        return services;
    }
}
