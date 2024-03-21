using Jpl.MicroService.Infrastructure.EventBus.Base.Abstractions;
using Jpl.MicroService.Infrastructure.EventBus.Base;
using Jpl.MicroService.Infrastructure.EventBus.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Jpl.MicroService.Infrastructure.EventBus.Base.Extensions;

namespace Jpl.MicroService.Infrastructure.EventBus;
internal static class Startup
{
    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        var eventBusSection = configuration.GetSection("EventBus");

        if (!eventBusSection.Exists())
        {
            return services;
        }

        services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

            var factory = new ConnectionFactory()
            {
                HostName = configuration.GetSection("EventBus:ConnectionString").Value,
                DispatchConsumersAsync = true
            };

            if (!string.IsNullOrEmpty(eventBusSection["UserName"]))
            {
                factory.UserName = eventBusSection["UserName"];
            }

            if (!string.IsNullOrEmpty(eventBusSection["Password"]))
            {
                factory.Password = eventBusSection["Password"];
            }

            int retryCount = eventBusSection.GetValue("RetryCount", 5);

            return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
        });

        services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
        {
            string subscriptionClientName = eventBusSection.GetRequiredValue("SubscriptionClientName");
            string brokerName = eventBusSection.GetRequiredValue("BrokerName");
            var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
            var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
            var eventBusSubscriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
            int retryCount = eventBusSection.GetValue("RetryCount", 5);

            return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, sp, eventBusSubscriptionsManager, brokerName, subscriptionClientName, retryCount);
        });

        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
        return services;
    }
}
