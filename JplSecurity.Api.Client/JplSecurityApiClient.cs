using JplSecurity.Contract;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace JplSecurity.Api.Client;

public class JplSecurityApiClient : IJplSecurityApiClient
{
    private readonly HttpClient _apiClient;
    private readonly ILogger<JplSecurityApiClient> _logger;
    private readonly string _baseUrl;

    public JplSecurityApiClient(HttpClient httpClient, ILogger<JplSecurityApiClient> logger, string serviceUrl)
    {
        _apiClient = httpClient;
        _logger = logger;
        _baseUrl = serviceUrl + "/api";
    }

    public async Task<GetPermissionByUserResponse> GetPermissionByUserAsync(GetPermissionByUserRequest request)
    {
        string uri = _baseUrl + "/grant";
        var content = new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, "application/json");
        var response = await _apiClient.PostAsync(uri, content);

        response.EnsureSuccessStatusCode();

        string permissionResponse = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<GetPermissionByUserResponse>(permissionResponse, JsonDefaults.CaseInsensitiveOptions);
        if (result == null) { return new GetPermissionByUserResponse(); }
        return result;
    }
}