using Dawn;

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
            _tokenValidator = Guard.Argument(tokenValidator, nameof(tokenValidator)).NotNull().Value;
        }

        public Task<ClaimsPrincipal> Handle(DecodeJsonWebBearerTokenRequest request,
            CancellationToken cancellationToken)
        {
            Guard.Argument(request, nameof(request)).NotNull();
            return _tokenValidator.ValidateAsync(request.Token, cancellationToken);
        }
    }
}
