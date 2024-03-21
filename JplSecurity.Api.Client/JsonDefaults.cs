using System.Text.Json;

namespace JplSecurity.Api.Client;

public static class JsonDefaults
{
    public static readonly JsonSerializerOptions CaseInsensitiveOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
}
