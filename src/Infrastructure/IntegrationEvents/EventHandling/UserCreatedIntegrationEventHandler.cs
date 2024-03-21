using Jpl.MicroService.Application.IntegrationEvents.Events;
using Jpl.MicroService.Infrastructure.EventBus.Base.Abstractions;
using Microsoft.Extensions.Logging;

namespace Jpl.MicroService.Infrastructure.IntegrationEvents.EventHandling;

public class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
{
    private readonly ILogger<UserCreatedIntegrationEventHandler> _logger;
    public UserCreatedIntegrationEventHandler(
        ILogger<UserCreatedIntegrationEventHandler> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(UserCreatedIntegrationEvent @event)
    {
        using (_logger.BeginScope(new List<KeyValuePair<string, object>> { new ("IntegrationEventContext", @event.Id) }))
        {
            _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

            await CreateUser(@event);

        }
    }

    private async Task CreateUser(UserCreatedIntegrationEvent @event)
    {
        // example of event handler
        // TODO: add code of handle stuff below

        _logger.LogInformation("{IntegrationEventHandler} - Create User: {UserEmail} ({@Items})", nameof(UserCreatedIntegrationEventHandler), @event);

    }
}
