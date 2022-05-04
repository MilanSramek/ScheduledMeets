using Dawn;

using MediatR;

using ScheduledMeets.Core;

using System.Security.Claims;

namespace ScheduledMeets.Business.UseCases.CreateUserByClaimsPrincipal;

public record CreateUserByClaimsPrincipalRequest : IRequest<User>
{
    public CreateUserByClaimsPrincipalRequest(ClaimsPrincipal principal)
        => Principal = Guard.Argument(principal, nameof(principal)).NotNull().Value;

    public ClaimsPrincipal Principal { get; }
}

