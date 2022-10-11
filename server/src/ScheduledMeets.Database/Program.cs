using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ScheduledMeets.Persistance;

using System.Reflection;

namespace ScheduledMeets.Database;

public class Program
{
    public static void Main(string[] args) => CreateHostBuilder(args);

    public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((_, configuration) => 
            configuration.AddUserSecrets(Assembly.GetExecutingAssembly()))
        .ConfigureServices((hostContext, services) =>
        {
            IConfiguration configuration = hostContext.Configuration;

            string connectionString = configuration.GetConnectionString("Database");
            string password = configuration.GetValue<string>("Database:Password");

            services
                .Configure<DbConnectionOptions>(_ =>
                {
                    _.ConnectionString = connectionString;
                    _.Password = password;
                })
                .AddAccessContext(_ => 
                    _.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
        });
}
