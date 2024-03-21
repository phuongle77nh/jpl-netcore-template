using Jpl.MicroService.Infrastructure.JplApiUrl;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Jpl.MicroService.Application.Common.Interfaces;

namespace Jpl.MicroService.Infrastructure.JplApiClient;

public class PermissionRequest
{
    public string Claim { get; set; }
}

public class PermissionResponse
{ 
    public bool Data { get; set; }
    public string Message { get; set; }
    public int StatusCode { get; set; }
}

public class JplApiClient : IJplApiClient
{
    private readonly HttpClient _apiClient;
    private readonly ILogger<JplApiClient> _logger;
    private readonly JplApiUrlSetting _rsApiUrlSetting;
    private string _rsAuthUrl => $"{_rsApiUrlSetting.JplAuth}/api";

    public JplApiClient(HttpClient httpClient, ILogger<JplApiClient> logger, IOptions<JplApiUrlSetting> rsApiUrlSetting)
    {
        _apiClient = httpClient;
        _logger = logger;
        _rsApiUrlSetting = rsApiUrlSetting.Value;
    }


    public async Task<bool> GetPermissionByUserAsync(PermissionRequest request)
    {
        string uri = _rsAuthUrl + "/permission";
        var content = new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, "application/json");
        var response = await _apiClient.PostAsync(uri, content);

        response.EnsureSuccessStatusCode();

        string permissionResponse = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<PermissionResponse>(permissionResponse, JsonDefaults.CaseInsensitiveOptions);
        if (result == null) { return false; }
        return result.Data;
    }
}