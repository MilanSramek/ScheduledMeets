using System.Security.Claims;

namespace ScheduledMeets.Business.OAuth;

public interface ITokenValidator
{
    Task<ClaimsPrincipal> ValidateAsync(string token, CancellationToken cancellationToken = default);
}
