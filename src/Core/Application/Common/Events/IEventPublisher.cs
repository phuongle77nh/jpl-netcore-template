using Jpl.MicroService.Application.Common.Interfaces;
using Jpl.MicroService.Shared.Events;

namespace Jpl.MicroService.Application.Common.Events;

public interface IEventPublisher : ITransientService
{
    Task PublishAsync(IEvent @event);
}