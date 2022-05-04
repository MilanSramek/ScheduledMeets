using Dawn;

using MediatR;

using ScheduledMeets.Core;

namespace ScheduledMeets.Business.UseCases.GetOrCreateUserByBearerToken;

public record GetOrCreateUserByBearerTokenRequest : IRequest<User>
{
    public GetOrCreateUserByBearerTokenRequest(string token)
    {
        Token = Guard.Argument(token, nameof(token)).NotNull().Value;
    }

    public string Token { get; }
}
