using Microsoft.AspNetCore.Builder;

namespace ScheduledMeets.Internals.Authorization
{
    public static class ApplicationBuilderExtensions
    {
        public static TApplicationBuilder UseDeveloperIdentity<TApplicationBuilder>(
            this TApplicationBuilder builder) where TApplicationBuilder : IApplicationBuilder
        {
            builder.UseMiddleware<AddDeveloperIdentityMiddleware>();
            return builder;
        }
    }
}
