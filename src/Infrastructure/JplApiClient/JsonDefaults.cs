using System.Text.Json;

namespace Jpl.MicroService.Infrastructure.JplApiClient;

public static class JsonDefaults
{
    public static readonly JsonSerializerOptions CaseInsensitiveOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
}
