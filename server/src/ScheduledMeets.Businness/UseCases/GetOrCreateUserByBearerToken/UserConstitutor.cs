using MediatR;

using ScheduledMeets.Business.Interfaces;
using ScheduledMeets.Business.OAuth;
using ScheduledMeets.Business.UseCases.CreateUserByClaimsPrincipal;
using ScheduledMeets.Core;

using System.Security.Claims;

namespace ScheduledMeets.Business.UseCases.GetOrCreateUserByBearerToken;

class UserConstitutor : IRequestHandler<GetOrCreateUserByBearerTokenRequest, User>
{
    private readonly IProcessor<CreateUserByClaimsPrincipalRequest, User> _userCreator;
    private readonly ITokenValidator _tokenValidator;
    private readonly IUserProvider _userProvider;

    public UserConstitutor(
        IProcessor<CreateUserByClaimsPrincipalRequest, User> userCreator,
        ITokenValidator tokenValidator,
        IUserProvider userProvider)
    {
        _userCreator = userCreator ?? throw new ArgumentNullException(nameof(userCreator));
        _tokenValidator = tokenValidator ?? throw new ArgumentNullException(nameof(tokenValidator));
        _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
    }

    public async Task<User> Handle(GetOrCreateUserByBearerTokenRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        ClaimsPrincipal principal = await _tokenValidator.ValidateAsync(request.Token,
            cancellationToken);
        User? user = await _userProvider.GetUserBy(principal, cancellationToken);

        if (user is { })
            return user;

        return await _userCreator.ProcessAsync(
            new CreateUserByClaimsPrincipalRequest(principal), cancellationToken);
    }
}
