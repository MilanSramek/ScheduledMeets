using ScheduledMeets.Core;

using System.Security.Claims;

namespace ScheduledMeets.Business.UseCases.GetOrCreateUserByBearerToken;

public interface IUserProvider
{
    Task<User?> GetUserBy(ClaimsPrincipal claimsPrincipal, CancellationToken cancellationToken = default);
}