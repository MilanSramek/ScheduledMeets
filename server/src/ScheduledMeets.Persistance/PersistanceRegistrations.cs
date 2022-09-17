using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

using ScheduledMeets.Business.Interfaces;

namespace ScheduledMeets.Persistance;

public static class PersistanceRegistrations
{
    public static IServiceCollection AddPersistance(
       this IServiceCollection services,
       Action<NpgsqlDbContextOptionsBuilder>? npgsqlOptionsAction = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        return services
            .AddAccessContext(npgsqlOptionsAction)
            .AddRepositories();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services) => services
        .AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>))
        .AddScoped(typeof(IRepository<>), typeof(Repository<>));


    private static IServiceCollection AddAccessContext(
        this IServiceCollection services,
        Action<NpgsqlDbContextOptionsBuilder>? npgsqlOptionsAction = null)
    {
        return services.AddDbContext<AccessContext>((serviceProvider, options) =>
        {
            (string connectionString, string password) = serviceProvider
                .GetRequiredService<IOptions<DbConnectionOptions>>()
                .Value;
            options
                .UseNpgsql(connectionString, opts =>
                {
                    npgsqlOptionsAction?.Invoke(opts);
                    opts.ProvidePasswordCallback((_, _, _, _) => password);
                })
                .UseSnakeCaseNamingConvention();
        });
    }
}