using RabbitMQ.Client;

namespace Jpl.MicroService.Infrastructure.EventBus.RabbitMQ;

public interface IRabbitMQPersistentConnection
    : IDisposable
{
    bool IsConnected { get; }

    bool TryConnect();

    IModel CreateModel();
}
