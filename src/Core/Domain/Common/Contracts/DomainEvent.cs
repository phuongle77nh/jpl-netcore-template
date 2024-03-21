using Jpl.MicroService.Shared.Events;

namespace Jpl.MicroService.Domain.Common.Contracts;

public abstract class DomainEvent : IEvent
{
    public DateTime TriggeredOn { get; protected set; } = DateTime.UtcNow;
}