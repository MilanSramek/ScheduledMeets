using MediatR;

using ScheduledMeets.Core;

using System.Security.Claims;

namespace ScheduledMeets.Business.UseCases.GetUserByClaimsPrincipal;

public record GetUserByClaimsPrincipalRequest : IRequest<User?>
{
    public GetUserByClaimsPrincipalRequest(ClaimsPrincipal claimsPrincipal)
    {
        ArgumentNullException.ThrowIfNull(claimsPrincipal);
        ClaimsPrincipal = claimsPrincipal;
    }

    public ClaimsPrincipal ClaimsPrincipal { get; }
}
