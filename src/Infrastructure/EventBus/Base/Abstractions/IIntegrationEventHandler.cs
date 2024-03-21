using Jpl.MicroService.Infrastructure.EventBus.Base.Events;

namespace Jpl.MicroService.Infrastructure.EventBus.Base.Abstractions;

public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
    where TIntegrationEvent : IntegrationEvent
{
    Task Handle(TIntegrationEvent @event);
}

public interface IIntegrationEventHandler
{
}
