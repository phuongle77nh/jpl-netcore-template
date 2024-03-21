using System.Net.Http.Headers;
using Jpl.MicroService.Infrastructure.JplApiUrl;
using Microsoft.Extensions.Options;

namespace Jpl.MicroService.Infrastructure.JplApiClient;

public class MailHttpClientAuthorizationDelegatingHandler
    : DelegatingHandler
{
    private readonly JplApiUrlSetting _rsApiUrlSetting;

    public MailHttpClientAuthorizationDelegatingHandler(IOptions<JplApiUrlSetting> rsApiUrlSetting)
    {
        _rsApiUrlSetting = rsApiUrlSetting.Value;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue(_rsApiUrlSetting.JplMailToken);
        return await base.SendAsync(request, cancellationToken);
    }
}
