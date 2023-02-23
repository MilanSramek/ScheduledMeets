using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

using ScheduledMeets.Business.Interfaces;
using ScheduledMeets.View;

namespace ScheduledMeets.Persistance;

public static class PersistanceRegistrations
{
    public static IServiceCollection AddPersistance(
       this IServiceCollection services,
       Action<NpgsqlDbContextOptionsBuilder>? npgsqlOptionsAction = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        return services
            .ConfigurePersistance()
            .AddAccessContext(npgsqlOptionsAction)
            .AddAccessProvider()
            .AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static IServiceCollection AddAccessContext(
        this IServiceCollection services,
        Action<NpgsqlDbContextOptionsBuilder>? npgsqlOptionsAction = null)
    {
        return services.AddDbContext<AccessContext>((serviceProvider, contextOptions) =>
        {
            (string connectionString, string password) = serviceProvider
                .GetRequiredService<IOptionsSnapshot<DbConnectionOptions>>()
                .Value;
            contextOptions
                .UseNpgsql(connectionString, opts =>
                {
                    npgsqlOptionsAction?.Invoke(opts);
                    opts.ProvidePasswordCallback((_, _, _, _) => password);
                })
                .UseSnakeCaseNamingConvention();
        });
    }

    private static IServiceCollection AddAccessProvider(this IServiceCollection services) => services
        .AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>))
        .AddScoped(typeof(IRepository<>), typeof(Repository<>))
        .AddScoped(typeof(IReader<>), typeof(ViewReader<>));

    private static IServiceCollection ConfigurePersistance(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddOptions<DbConnectionOptions>()
            .Configure<IConfiguration>((connectionOptions, configuration) =>
            {
                connectionOptions.ConnectionString = configuration.GetConnectionString("Database");
                connectionOptions.Password = configuration.GetValue<string>("Database:Password");
            });

        return services;
    }
}