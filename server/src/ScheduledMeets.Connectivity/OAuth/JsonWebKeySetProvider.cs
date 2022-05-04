using IdentityModel.Client;

using Microsoft.Extensions.Options;

using ScheduledMeets.Business.Interfaces;
using ScheduledMeets.Connectivity.Helpers;
using ScheduledMeets.Connectivity.Tools;
using ScheduledMeets.Internals;
using ScheduledMeets.Internals.Extensions;

namespace ScheduledMeets.Connectivity.OAuth;

public class JsonWebKeySetProvider : IProvider<JsonWebKeySetResponse>, IDisposable
{
    private readonly IProvider<DiscoveryDocumentResponse> _discoveryProvider;
    private readonly IHttpClientFactory _clientFactory;
    private readonly ITimeProvider _timeProvider;
    private readonly IDisposable _optionSubscription;

    private Fresh<WithValidity<JsonWebKeySetResponse>> _keySetCache;

    public JsonWebKeySetProvider(
        IOptionsMonitor<JsonWebKeySetProviderSettings> options,
        IProvider<DiscoveryDocumentResponse> discoveryProvider,
        IHttpClientFactory clientFactory,
        ITimeProvider timeProvider)
    {
        if (clientFactory is null) throw new ArgumentNullException(nameof(clientFactory));
        if (discoveryProvider is null) throw new ArgumentNullException(nameof(discoveryProvider));
        if (options is null) throw new ArgumentNullException(nameof(options));
        if (timeProvider is null) throw new ArgumentNullException(nameof(timeProvider));

        _clientFactory = clientFactory;
        _discoveryProvider = discoveryProvider;

        _keySetCache = GetKeySetCache(options.CurrentValue);
        _optionSubscription = options.OnChange(settings =>
            _keySetCache = GetKeySetCache(settings));
        _timeProvider = timeProvider;
    }

    public async Task<JsonWebKeySetResponse> GetAsync(
        CancellationToken cancellationToken = default)
    {
        (JsonWebKeySetResponse response, _) = await _keySetCache.GetAsync(cancellationToken);
        return response;
    }

    public void Dispose() => _optionSubscription.Dispose();

    private Fresh<WithValidity<JsonWebKeySetResponse>> GetKeySetCache(
        JsonWebKeySetProviderSettings settings)
    {
        TimeSpan? cacheDuration = settings.CacheDuration;

        return new Fresh<WithValidity<JsonWebKeySetResponse>>(
            () => GetKeySetAsync(cacheDuration),
            ValidateKeySet);
    }

    private async Task<WithValidity<JsonWebKeySetResponse>> GetKeySetAsync(
        TimeSpan? cacheDuration)
    {
        DiscoveryDocumentResponse discovery = await _discoveryProvider.GetAsync();
        if (!discovery.IsError)
            return await FetchKeySetAsync(discovery.JwksUri, cacheDuration);

        JsonWebKeySetResponse errroResponse = discovery.Exception is Exception exception
            ? ProtocolResponse.FromException<JsonWebKeySetResponse>(exception,
                discovery.Error)
            : await ProtocolResponse.FromHttpResponseAsync<JsonWebKeySetResponse>(
                discovery.HttpResponse, discovery.Error);
        return new(errroResponse, default);
    }

    private async Task<WithValidity<JsonWebKeySetResponse>> FetchKeySetAsync(
        string jwksUri,
        TimeSpan? cacheDuration)
    {
        HttpClient client = _clientFactory.CreateClient();

        JsonWebKeySetResponse response = await client.GetJsonWebKeySetAsync(jwksUri);
        DateTime? validTo = response.HttpResponse.IsSuccessStatusCode
            ? _timeProvider.GetCurrentTime() + (cacheDuration
                ?? response.HttpResponse.CacheDuration())
            : default;

        return new(response, validTo);
    }

    private bool ValidateKeySet(WithValidity<JsonWebKeySetResponse> keySet) =>
        keySet.ValidTo > _timeProvider.GetCurrentTime();
}
