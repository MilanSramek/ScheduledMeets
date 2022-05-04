using IdentityModel;
using IdentityModel.Client;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using ScheduledMeets.Business.Interfaces;
using ScheduledMeets.Internals.Extensions;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ScheduledMeets.Business.OAuth;

class JsonWebTokenValidator : ITokenValidator
{
    private readonly IProvider<DiscoveryDocumentResponse> _discoveryProvider;
    private readonly IProvider<JsonWebKeySetResponse> _keySetProvider;
    private readonly JsonWebTokenValidatorSettings _settings;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public JsonWebTokenValidator(
        IProvider<DiscoveryDocumentResponse> discoveryProvider,
        IProvider<JsonWebKeySetResponse> keySetProvider,
        IOptionsSnapshot<JsonWebTokenValidatorSettings> options)
    {
        if (discoveryProvider is null) throw new ArgumentNullException(nameof(discoveryProvider));
        if (keySetProvider is null) throw new ArgumentNullException(nameof(keySetProvider));
        if (options is null) throw new ArgumentNullException(nameof(options));

        _discoveryProvider = discoveryProvider
            ?? throw new ArgumentNullException(nameof(discoveryProvider));
        _keySetProvider = keySetProvider ?? throw new ArgumentNullException(nameof(keySetProvider));
        _settings = options.Value;
        _tokenHandler = new();
    }

    public async Task<ClaimsPrincipal> ValidateAsync(string token,
        CancellationToken cancellationToken = default)
    {
        (DiscoveryDocumentResponse discovery, JsonWebKeySetResponse keySet) = await
                _discoveryProvider.GetAsync(cancellationToken)
            .And(
                _keySetProvider.GetAsync(cancellationToken));

        if ((Validate(discovery) ?? Validate(keySet)) is Exception exception)
            throw exception;

        TokenValidationParameters validationParameters = GetValidationParameters(discovery.Issuer,
            keySet.KeySet.Keys);
        return _tokenHandler.ValidateToken(token, validationParameters, out _);
    }

    private TokenValidationParameters GetValidationParameters(string issuer,
         IEnumerable<IdentityModel.Jwk.JsonWebKey> keys)
    {
        IEnumerable<RsaSecurityKey> securityKeys = keys
            .Select(webKey => new RsaSecurityKey(new RSAParameters
            {
                Exponent = Base64Url.Decode(webKey.E),
                Modulus = Base64Url.Decode(webKey.N)
            })
            {
                KeyId = webKey.Kid
            });

        return new()
        {
            ValidIssuer = issuer,
            ValidAudience = _settings.Audience,
            IssuerSigningKeys = securityKeys,

            NameClaimType = ClaimType.Email,

            RequireSignedTokens = true,
            ValidateLifetime = true
        };
    }

    private static Exception? Validate(ProtocolResponse response) => response.ErrorType switch
    {
        ResponseErrorType.None => null,
        ResponseErrorType.Exception => response.Exception,
        _ => new ApplicationException(response.Error)
    };
}
