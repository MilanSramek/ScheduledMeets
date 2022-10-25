using MediatR;

using ScheduledMeets.Core;

namespace ScheduledMeets.Business.UseCases.GetOrCreateUserByBearerToken;

public record GetOrCreateUserByBearerTokenRequest : IRequest<User>
{
    public GetOrCreateUserByBearerTokenRequest(string token)
    {
        Token = token ?? throw new ArgumentNullException(nameof(token));
    }

    public string Token { get; }
}
