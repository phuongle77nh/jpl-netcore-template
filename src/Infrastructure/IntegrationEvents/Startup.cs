using Jpl.MicroService.Infrastructure.EventBus.Base.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Jpl.MicroService.Application.IntegrationEvents.Events;
using Jpl.MicroService.Infrastructure.IntegrationEvents.EventHandling;
using Microsoft.AspNetCore.Builder;

namespace Jpl.MicroService.Infrastructure.IntegrationEvents;
internal static class Startup
{
    public static IServiceCollection AddIntegrationEvents(this IServiceCollection services)
    {
        //services.AddTransient<IIntegrationEventHandler<UserCreatedIntegrationEvent>, UserCreatedIntegrationEventHandler>();
        return services;
    }

    public static IApplicationBuilder SubcribeIntegrationEvents(this IApplicationBuilder app)
    {

        var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
        //eventBus.Subscribe<UserCreatedIntegrationEvent, IIntegrationEventHandler<UserCreatedIntegrationEvent>>();
        return app;
    }
}
