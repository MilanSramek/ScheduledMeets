using Microsoft.AspNetCore.Authentication.Cookies;

namespace ScheduledMeets.Internals.Authentication
{
    public static class CookieAuthenticationOptionsExtensions
    {
        public static void DisableOnChallangeRedirection(this CookieAuthenticationOptions options)
        {
            if (options is null) throw new ArgumentNullException(nameof(options));
            options.LoginPath = null;
        }

        public static void DisableOnForbiddenRedirection(this CookieAuthenticationOptions options)
        {
            if (options is null) throw new ArgumentNullException(nameof(options));
            options.AccessDeniedPath = null;
        }
    }
}
