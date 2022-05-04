using System.Net.Http.Headers;

namespace ScheduledMeets.Internals.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static TimeSpan? CacheDuration(this HttpResponseMessage response)
        {
            if (response is null) throw new ArgumentNullException(nameof(response));
            if (response.Headers.CacheControl is not CacheControlHeaderValue cacheControl
                || cacheControl.NoStore
                || cacheControl.NoCache)
            {
                return default;
            }

            return (cacheControl.MaxAge ?? cacheControl.SharedMaxAge)
                - (response.Headers.Age ?? TimeSpan.Zero);
        }
    }
}
