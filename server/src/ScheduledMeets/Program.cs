using Microsoft.AspNetCore.Builder;

using ScheduledMeets.Business;
using ScheduledMeets.Persistance;

using ScheduleMeets.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure()
    .AddBusiness()
    .AddPersistance();


WebApplication? application = builder.Build();
application?.Run();
