using Microsoft.Kiota.Abstractions.Authentication;

namespace GraphLogger.Infrastructure.Services;

internal class AccessTokenAuthenticationProvider : IAccessTokenProvider
{
    private readonly AzureAD _azureAD;

    public AccessTokenAuthenticationProvider(AzureAD azureAD)
    {
        _azureAD = azureAD;
    }

    public AllowedHostsValidator AllowedHostsValidator { get; }

    public Task<string> GetAuthorizationTokenAsync(Uri uri, Dictionary<string, object> additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
    {
        // You can get access token directly by send request to azure AD
        return Task.FromResult(_azureAD.AccessToken);
    }
}
