using MediatR;

using System.Security.Claims;

namespace ScheduledMeets.Business.UseCases.DecodeJsonWebBearerToken;

public record DecodeJsonWebBearerTokenRequest : IRequest<ClaimsPrincipal>
{
    public DecodeJsonWebBearerTokenRequest(string token) => Token = token ??
        throw new ArgumentNullException(nameof(token));

    public string Token { get; }
}
