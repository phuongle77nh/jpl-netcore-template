using Jpl.MicroService.Infrastructure.JplApiUrl;
using Microsoft.Extensions.DependencyInjection;

namespace Jpl.MicroService.Infrastructure.JplApiClient;

internal static class Startup
{
    internal static IServiceCollection AddJplHttpClient(this IServiceCollection services)
    {
        // api url option
        services.AddOptions<JplApiUrlSetting>()
            .BindConfiguration(nameof(JplApiUrlSetting));

        services.AddOptions<JplWebUrlSetting>()
            .BindConfiguration(nameof(JplWebUrlSetting));

        // Register delegating handlers
        services.AddHttpContextAccessor();
        services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
        services.AddTransient<MailHttpClientAuthorizationDelegatingHandler>();

        // Register http services
        services.AddHttpClient<IJplApiClient, JplApiClient>()
           .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient("JplMailApiClient")
           .AddHttpMessageHandler<MailHttpClientAuthorizationDelegatingHandler>();
        return services;
    }
}