using Dawn;

using MediatR;

using ScheduledMeets.Core;

using System.Security.Claims;

namespace ScheduledMeets.Business.UseCases.GetUserByClaimsPrincipal;

public record GetUserByClaimsPrincipalRequest : IRequest<User?>
{
    public GetUserByClaimsPrincipalRequest(ClaimsPrincipal claimsPrincipal)
    {
        ClaimsPrincipal = Guard.Argument(claimsPrincipal, nameof(claimsPrincipal)).NotNull().Value;
    }

    public ClaimsPrincipal ClaimsPrincipal { get; }
}
