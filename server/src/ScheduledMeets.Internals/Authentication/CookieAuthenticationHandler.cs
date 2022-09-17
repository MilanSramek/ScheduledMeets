using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System.Text.Encodings.Web;

namespace ScheduledMeets.Internals.Authentication;

public class CookieAuthenticationHandler : Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationHandler
{
    public CookieAuthenticationHandler(
        IOptionsMonitor<CookieAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        if (string.IsNullOrWhiteSpace(Options.ForwardChallenge))
        {
            Response.StatusCode = 401;
            return Task.CompletedTask;
        }

        return base.HandleChallengeAsync(properties);
    }

    protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
    {
        if (string.IsNullOrWhiteSpace(Options.ForwardForbid))
        {
            Response.StatusCode = 403;
            return Task.CompletedTask;
        }

        return base.HandleForbiddenAsync(properties);
    }
}
