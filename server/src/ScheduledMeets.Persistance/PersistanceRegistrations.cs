using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace ScheduledMeets.Persistance;

public static class PersistanceRegistrations
{
    public static IServiceCollection AddPersistance(
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