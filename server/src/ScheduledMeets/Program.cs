using Microsoft.AspNetCore.Builder;

using ScheduledMeets;
using ScheduledMeets.Business;
using ScheduledMeets.Connectivity;
using ScheduledMeets.Infrastructure;
using ScheduledMeets.Persistance;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure()
    .AddBusiness()
    .AddPersistance()
    .AddConnectivity()
    .AddCommonServices();


WebApplication? application = builder.Build();
application?.Run();
