using System.Reflection;
using System.Runtime.CompilerServices;
using Jpl.MicroService.Infrastructure.Auth;
using Jpl.MicroService.Infrastructure.BackgroundJobs;
using Jpl.MicroService.Infrastructure.Caching;
using Jpl.MicroService.Infrastructure.Common;
using Jpl.MicroService.Infrastructure.Cors;
using Jpl.MicroService.Infrastructure.FileStorage;
using Jpl.MicroService.Infrastructure.Localization;
using Jpl.MicroService.Infrastructure.Mapping;
using Jpl.MicroService.Infrastructure.Middleware;
using Jpl.MicroService.Infrastructure.Notifications;
using Jpl.MicroService.Infrastructure.OpenApi;
using Jpl.MicroService.Infrastructure.Persistence;
using Jpl.MicroService.Infrastructure.SecurityHeaders;
using Jpl.MicroService.Infrastructure.Validations;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Finbuckle.MultiTenant;
using Jpl.MicroService.Infrastructure.Multitenancy;
using Jpl.MicroService.Infrastructure.EventBus;
using Jpl.MicroService.Infrastructure.Persistence.Initialization;
using Jpl.MicroService.Infrastructure.ApplicationInsight;
using Jpl.MicroService.Infrastructure.JplApiClient;
using Jpl.MicroService.Infrastructure.IntegrationEvents;

[assembly: InternalsVisibleTo("Infrastructure.Test")]

namespace Jpl.MicroService.Infrastructure;

public static class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var applicationAssembly = typeof(Jpl.MicroService.Application.Startup).GetTypeInfo().Assembly;
        MapsterSettings.Configure();

        services.AddScoped<ITenantInfo, FSHTenantInfo>();

        return services
            .AddApiVersioning()
            .AddAuth(config)
            .AddBackgroundJobs(config)
            .AddCaching(config)
            .AddCorsPolicy(config)
            .AddExceptionMiddleware()
            .AddBehaviours(applicationAssembly)
            .AddHealthCheck()
            .AddPOLocalization(config)
            .AddMediatR(Assembly.GetExecutingAssembly())
            .AddNotifications(config)
            .AddOpenApiDocumentation(config)
            .AddPersistence()
            .AddRequestLogging(config)
            .AddRouting(options => options.LowercaseUrls = true)
            .AddApplicationInsight(config)
            .AddJplHttpClient()
            .AddServices()
            .AddEventBus(config)
            .AddIntegrationEvents()
            ;
    }

    private static IServiceCollection AddApiVersioning(this IServiceCollection services) =>
        services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
        });

    private static IServiceCollection AddHealthCheck(this IServiceCollection services) =>
        services.AddHealthChecks().Services;

    public static async Task InitializeDatabasesAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        // Create a new scope to retrieve scoped services
        using var scope = services.CreateScope();

        await scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>()
            .InitializeDatabasesAsync(cancellationToken);
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration config) =>
        builder
            .UseRequestLocalization()
            .UseStaticFiles()
            .UseSecurityHeaders(config)
            .UseFileStorage()
            .UseExceptionMiddleware()
            .UseRouting()
            .UseCorsPolicy()
            .UseAuthentication()
            .UseCurrentUser()
            .UseAuthorization()
            .UseRequestLogging(config)
            .UseHangfireDashboard(config)
            .UseOpenApiDocumentation(config);

    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapControllers().RequireAuthorization();
        builder.MapHealthCheck();
        builder.MapNotifications();
        return builder;
    }

    private static IEndpointConventionBuilder MapHealthCheck(this IEndpointRouteBuilder endpoints) =>
        endpoints.MapHealthChecks("/api/health");
}