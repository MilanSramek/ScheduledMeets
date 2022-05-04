using Dawn;

using MediatR;

using ScheduledMeets.Business.Interfaces;
using ScheduledMeets.Business.UseCases.CreateUserByClaimsPrincipal;
using ScheduledMeets.Business.UseCases.DecodeJsonWebBearerToken;
using ScheduledMeets.Business.UseCases.GetUserByClaimsPrincipal;
using ScheduledMeets.Core;

using System.Security.Claims;

namespace ScheduledMeets.Business.UseCases.GetOrCreateUserByBearerToken;

class UserConstitutor : IRequestHandler<GetOrCreateUserByBearerTokenRequest, User>
{
    private readonly IProcessor<DecodeJsonWebBearerTokenRequest, ClaimsPrincipal> _tokenValidator;
    private readonly IProcessor<GetUserByClaimsPrincipalRequest, User?> _userProvider;
    private readonly IProcessor<CreateUserByClaimsPrincipalRequest, User> _userCreator;

    public UserConstitutor(
        IProcessor<DecodeJsonWebBearerTokenRequest, ClaimsPrincipal> tokenValidator,
        IProcessor<GetUserByClaimsPrincipalRequest, User?> userProvider,
        IProcessor<CreateUserByClaimsPrincipalRequest, User> userCreator)
    {
        _tokenValidator = Guard.Argument(tokenValidator).NotNull().Value;
        _userProvider = Guard.Argument(userProvider).NotNull().Value;
        _userCreator = Guard.Argument(userCreator).NotNull().Value;
    }

    public async Task<User> Handle(GetOrCreateUserByBearerTokenRequest request,
        CancellationToken cancellationToken = default)
    {
        Guard.Argument(request, nameof(request)).NotNull();

        ClaimsPrincipal principal = await _tokenValidator
            .ProcessAsync(new DecodeJsonWebBearerTokenRequest(request.Token), cancellationToken);

        User? user = await _userProvider
                .ProcessAsync(new GetUserByClaimsPrincipalRequest(principal), cancellationToken);
        if (user is not null)
            return user;

        return await _userCreator
            .ProcessAsync(new CreateUserByClaimsPrincipalRequest(principal), cancellationToken);
    }
}
