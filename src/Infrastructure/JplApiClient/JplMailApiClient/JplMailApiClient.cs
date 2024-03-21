using Jpl.MicroService.Infrastructure.JplApiUrl;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Jpl.MicroService.Infrastructure.JplApiClient;

namespace Jpl.MicroService.Infrastructure.JplMailApiClient;

public class MailAddress
{
    public string name { get; set; } = string.Empty;
    public string address { get; set; } = string.Empty;
}

public class MailResponseRaw
{
    public string Type { get; set; } = string.Empty;
}

public class MailResponseEnvelope
{
    public string From { get; set; } = string.Empty;
    public List<string> To { get; set; } = new List<string>();

}

public class MailRequest
{
    public MailAddress from { get; set; } = new MailAddress();
    public List<MailAddress> to { get; set; } = new List<MailAddress>();
    public List<MailAddress> cc { get; set; }
    public string subject { get; set; } = string.Empty;
    public string html { get; set; } = string.Empty;
    public string tenantId { get; set; } = string.Empty;
    public string templateKey { get; set; } = string.Empty;
}

public class MailResponse
{ 
    public MailResponseEnvelope Envelope { get; set; }
    public string MessageId { get; set; } = string.Empty;
    public string Response { get; set; } = string.Empty;
    public MailResponseRaw Raw { get; set; }

}

public class JplMailApiClient : IJplMailApiClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<JplMailApiClient> _logger;
    private readonly JplApiUrlSetting _rsApiUrlSetting;
    private string _baseUrl => _rsApiUrlSetting.JplMail;

    public JplMailApiClient(
        IHttpClientFactory httpClientFactory,
        ILogger<JplMailApiClient> logger,
        IOptions<JplApiUrlSetting> rsApiUrlSetting)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _rsApiUrlSetting = rsApiUrlSetting.Value;
    }

    public async Task<bool> SendMail(MailRequest request)
    {
        var apiClient = _httpClientFactory.CreateClient("JplMailApiClient");

        string uri = _baseUrl;
        var content = new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, "application/json");
        var response = await apiClient.PostAsync(uri, content);

        string responseString = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<MailResponse>(responseString, JsonDefaults.CaseInsensitiveOptions);
        if (result == null) { return false; }
        return !string.IsNullOrEmpty(result.MessageId);
    }
}