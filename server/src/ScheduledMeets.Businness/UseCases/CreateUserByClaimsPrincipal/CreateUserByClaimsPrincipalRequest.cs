using Dawn;

using MediatR;

using ScheduledMeets.Core;

using System.Security.Claims;

namespace ScheduledMeets.Business.UseCases.CreateUserByClaimsPrincipal;

public record CreateUserByClaimsPrincipalRequest : IRequest<User>
{
    public CreateUserByClaimsPrincipalRequest(ClaimsPrincipal claimsPrincipal)
        => ClaimsPrincipal = Guard.Argument(claimsPrincipal, nameof(claimsPrincipal)).NotNull().Value;

    public ClaimsPrincipal ClaimsPrincipal { get; }
}

