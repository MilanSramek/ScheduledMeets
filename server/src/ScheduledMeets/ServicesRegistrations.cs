using MediatR;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

using ScheduledMeets.Internals.Authentication;

namespace ScheduledMeets;

static class ServicesRegistrations
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));

        services
            .AddSecurity()
            .AddMediator();
        return services
            .AddHttpClient()
            .AddHttpContextAccessor();
    }

    private static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        services
            .AddAuthorization()
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookieInternal(options =>
                {
                    options.DisableOnChallangeRedirection();
                    options.DisableOnForbiddenRedirection();
                    options.Cookie.HttpOnly = true;
                });

        return services;
    }

    private static IServiceCollection AddMediator(this IServiceCollection services) => services
        .AddScoped<ISender, Mediator>();
}
