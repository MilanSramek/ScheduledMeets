using IdentityModel.Client;

using Microsoft.Extensions.Options;

using ScheduledMeets.Business.Interfaces;
using ScheduledMeets.Connectivity.Helpers;
using ScheduledMeets.Connectivity.Tools;
using ScheduledMeets.Internals;
using ScheduledMeets.Internals.Extensions;

namespace ScheduledMeets.Connectivity.OAuth;

public sealed class DiscoveryDocumentProvider : IProvider<DiscoveryDocumentResponse>, IDisposable
{
    private static class ErrorMesage
    {
        public static string From(HttpResponseMessage response)
            => $"Error connecting to {response.RequestMessage?.RequestUri}: " +
                $"{(int)response.StatusCode}-{response.StatusCode} {response.ReasonPhrase}";

        public static string From(Exception exception, string url)
            => $"Error connecting to {url}. {exception.Message}.";
    }

    private readonly IHttpClientFactory _clientFactory;
    private readonly ITimeProvider _timeProvider;
    private readonly IDisposable _optionSubscription;

    private Fresh<WithValidity<DiscoveryDocumentResponse>> _discoveryCache;

    public DiscoveryDocumentProvider(
        IOptionsMonitor<DiscoveryDocumentProviderSettings> options,
        IHttpClientFactory clientFactory,
        ITimeProvider timeProvider)
    {
        if (clientFactory is null) throw new ArgumentNullException(nameof(clientFactory));
        if (options is null) throw new ArgumentNullException(nameof(options));
        if (timeProvider is null) throw new ArgumentNullException(nameof(timeProvider));

        _clientFactory = clientFactory;

        _discoveryCache = GetDiscoveryCache(options.CurrentValue);
        _optionSubscription = options.OnChange(settings =>
            _discoveryCache = GetDiscoveryCache(settings));
        _timeProvider = timeProvider;
    }

    public async Task<DiscoveryDocumentResponse> GetAsync(
        CancellationToken cancellationToken = default)
    {
        (DiscoveryDocumentResponse response, _) = await _discoveryCache.GetAsync(cancellationToken);
        return response;
    }

    public void Dispose() => _optionSubscription.Dispose();

    private Fresh<WithValidity<DiscoveryDocumentResponse>> GetDiscoveryCache(
        DiscoveryDocumentProviderSettings settings)
    {
        DiscoveryEndpoint endpoint = DiscoveryEndpoint.ParseUrl(settings.Authority,
           settings.DiscoveryDocumentPath);
        DiscoveryPolicy policy = new()
        {
            Authority = settings.Authority,
            DiscoveryDocumentPath = settings.DiscoveryDocumentPath,
            AdditionalEndpointBaseAddresses = settings.AdditionalEndpointBaseAddresses,
            RequireHttps = true,
            AllowHttpOnLoopback = false,
            ValidateIssuerName = true,
            ValidateEndpoints = true,
        };
        TimeSpan? cacheDuration = settings.CacheDuration;

        return new Fresh<WithValidity<DiscoveryDocumentResponse>>(
            () => FetchDiscoveryAsync(endpoint, policy, cacheDuration),
            ValidateDiscovery);
    }

    private async Task<WithValidity<DiscoveryDocumentResponse>> FetchDiscoveryAsync(
        DiscoveryEndpoint endpoint,
        DiscoveryPolicy policy,
        TimeSpan? cacheDuration)
    {
        HttpRequestMessage request = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(endpoint.Url),
        };
        HttpClient client = _clientFactory.CreateClient();
        try
        {
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                DiscoveryDocumentResponse discoveryResponse = await ProtocolResponse
                    .FromHttpResponseAsync<DiscoveryDocumentResponse>(response, policy);
                DateTime? validTo = _timeProvider.GetCurrentTime() +
                    (cacheDuration ?? response.CacheDuration());

                return new(discoveryResponse, validTo);
            }

            return new(await ProtocolResponse.FromHttpResponseAsync<DiscoveryDocumentResponse>(
                response, ErrorMesage.From(response)), default);
        }
        catch (Exception exception)
        {
            return new(ProtocolResponse.FromException<DiscoveryDocumentResponse>(exception,
                ErrorMesage.From(exception, endpoint.Url)), default);
        }
    }

    private bool ValidateDiscovery(WithValidity<DiscoveryDocumentResponse> discovery) =>
        discovery.ValidTo > _timeProvider.GetCurrentTime();
}
