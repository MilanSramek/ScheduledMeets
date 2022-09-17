using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

using ScheduledMeets;
using ScheduledMeets.Business;
using ScheduledMeets.Connectivity;
using ScheduledMeets.GraphQL;
using ScheduledMeets.Infrastructure;
using ScheduledMeets.Internals.Authorization;
using ScheduledMeets.Persistance;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure()
    .AddBusiness()
    .AddPersistance()
    .AddConnectivity()
    .AddCommonServices()
    .AddGraphQL();

WebApplication application = builder.Build();

if (application.Environment.IsDevelopment())
    application.UseDeveloperIdentity();

application
    .UseGraphQL()
    .Run();