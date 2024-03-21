using Jpl.MicroService.Infrastructure.EventBus.Base.Events;

namespace Jpl.MicroService.Application.IntegrationEvents.Events;

// Integration Events notes:
// An Event is “something that has happened in the past”, therefore its name has to be past tense
// An Integration Event is an event that can cause side effects to other microservices, Bounded-Contexts or external systems.
public record UserCreatedIntegrationEvent : IntegrationEvent
{
    public string UserId { get; private init; }

    public string LastName { get; private init; }

    public string FirstName { get; private init; }

    public string Email { get; private init; }
    public string Tenant { get; private init; }

    public UserCreatedIntegrationEvent(string userId, string lastName, string firstName, string email, string tenant)
    {
        UserId = userId;
        LastName = lastName;
        FirstName = firstName;
        Email = email;
        Tenant = tenant;
    }
}
