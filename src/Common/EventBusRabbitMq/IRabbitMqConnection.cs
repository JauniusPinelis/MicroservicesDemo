using RabbitMQ.Client;
using System;

namespace EventBusRabbitMq
{
    public interface IRabbitMqConnection : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
