using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using ScheduledMeets;
using ScheduledMeets.Api;
using ScheduledMeets.Business;
using ScheduledMeets.Connectivity;
using ScheduledMeets.Infrastructure;
using ScheduledMeets.Internals.Authorization;
using ScheduledMeets.Persistence;
using ScheduledMeets.View;

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
        .AddView()
        .AddApi();

    using WebApplication application = builder.Build();

    application
        .UseHttpLogging();

    if (application.Environment.IsDevelopment())
        application.UseDeveloperIdentity();

    application
        .UseRouting()
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