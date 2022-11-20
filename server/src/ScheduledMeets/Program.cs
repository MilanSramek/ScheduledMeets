using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using ScheduledMeets;
using ScheduledMeets.Business;
using ScheduledMeets.Connectivity;
using ScheduledMeets.GraphQL;
using ScheduledMeets.Infrastructure;
using ScheduledMeets.Internals.Authorization;
using ScheduledMeets.Persistance;

using Serilog;
using Serilog.Core;

using System.Reflection;

Logger logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("log.txt")
    .CreateLogger();

try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
    builder.Logging
        .ClearProviders()
        .AddSerilog(logger, true);

    if (builder.Environment.IsDevelopment())
        builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly());

    builder.Services
        .AddInfrastructure()
        .AddBusiness(builder.Configuration)
        .AddPersistance()
        .AddConnectivity(builder.Configuration)
        .AddCommonServices()
        .AddGraphQL();

    using WebApplication application = builder.Build();

    application
        .UseHttpLogging();

    if (application.Environment.IsDevelopment())
        application.UseDeveloperIdentity();

    application
        .UseAuthentication()
        .UseAuthorization();
    application
        .UseGraphQL(application.Environment);

    application
        .Run();
}
catch (Exception exception)
{
    logger.Fatal(exception, "Host terminated unexpectedly");
}