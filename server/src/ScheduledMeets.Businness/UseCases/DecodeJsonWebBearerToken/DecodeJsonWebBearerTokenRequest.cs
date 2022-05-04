using Dawn;

using MediatR;

using System.Security.Claims;

namespace ScheduledMeets.Business.UseCases.DecodeJsonWebBearerToken;

public record DecodeJsonWebBearerTokenRequest : IRequest<ClaimsPrincipal>
{
    public DecodeJsonWebBearerTokenRequest(string token)
        => Token = Guard.Argument(token, nameof(token)).NotNull().Value;

    public string Token { get; }
}
