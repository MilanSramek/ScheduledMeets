using MediatR;

using ScheduledMeets.Business.OAuth;

using System.Security.Claims;

namespace ScheduledMeets.Business.UseCases.DecodeJsonWebBearerToken
{
    class JsonWebBearerTokenHandler : IRequestHandler<DecodeJsonWebBearerTokenRequest, ClaimsPrincipal>
    {
        private readonly ITokenValidator _tokenValidator;

        public JsonWebBearerTokenHandler(ITokenValidator tokenValidator)
        {
            _tokenValidator = tokenValidator 
                ?? throw new ArgumentNullException(nameof(tokenValidator));
        }

        public Task<ClaimsPrincipal> Handle(DecodeJsonWebBearerTokenRequest request,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            return _tokenValidator.ValidateAsync(request.Token, cancellationToken);
        }
    }
}
