using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

using ScheduledMeets.Business.Interfaces;
using ScheduledMeets.Persistence.Model;
using ScheduledMeets.View;

namespace ScheduledMeets.Persistence;

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
            .AddModel()
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
        .AddUserProviders();

    private static IServiceCollection AddUserProviders(this IServiceCollection services) => services
        .AddScoped(typeof(IReadRepository<Core.User>), typeof(ReadRepository<Core.User, User>))
        .AddScoped(typeof(IRepository<Core.User>), typeof(Repository<Core.User, User>))
        .AddScoped(typeof(IReader<UserView>), typeof(ViewReader<UserView, User>));

    private static IServiceCollection ConfigurePersistance(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddOptions<DbConnectionOptions>()
            .Configure<IConfiguration>((connectionOptions, configuration) =>
            {
                connectionOptions.ConnectionString = configuration.GetConnectionString("Database")
                    ?? throw new Exception("Unable to retrieve connection string.");
                connectionOptions.Password = configuration.GetValue<string>("Database:Password")
                    ?? throw new Exception("Unable to retrive database password.");
            });

        return services;
    }
}