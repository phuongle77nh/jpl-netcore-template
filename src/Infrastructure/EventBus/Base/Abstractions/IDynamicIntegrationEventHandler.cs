namespace Jpl.MicroService.Infrastructure.EventBus.Base.Abstractions;

public interface IDynamicIntegrationEventHandler
{
    Task Handle(dynamic eventData);
}
