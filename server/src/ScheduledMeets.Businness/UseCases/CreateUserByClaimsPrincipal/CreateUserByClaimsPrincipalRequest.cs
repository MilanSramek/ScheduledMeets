using MediatR;
using ScheduledMeets.Business.RequestPipeline;
using ScheduledMeets.Core;

using System.Security.Claims;

namespace ScheduledMeets.Business.UseCases.CreateUserByClaimsPrincipal;

public record CreateUserByClaimsPrincipalRequest : IDataActiveRequest<User>
{
    public CreateUserByClaimsPrincipalRequest(ClaimsPrincipal claimsPrincipal) => ClaimsPrincipal = 
        claimsPrincipal ?? throw new ArgumentNullException(nameof(claimsPrincipal));

    public ClaimsPrincipal ClaimsPrincipal { get; }
}