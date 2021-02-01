using AutoMapper;
using EventBusRabbitMq;
using EventBusRabbitMq.Common;
using EventBusRabbitMq.Events;
using MediatR;
using Newtonsoft.Json;
using Ordering.Application.Commands;
using Ordering.Core.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Ordering.Api.RabbitMq
{
    // This counsumer is for particular event
    public class EventBusRabbitMqConsumer
    {
        private readonly IRabbitMqConnection _connection;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repository;

        public EventBusRabbitMqConsumer(IRabbitMqConnection connection, IMediator mediator, IMapper mapper, IOrderRepository repository)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Consume()
        {
            var channel = _connection.CreateModel();
            channel.QueueDeclare(EventBusConstants.BasketCheckoutQueue, false, false, false, null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += ReceivedEvent;

            channel.BasicConsume(queue: EventBusConstants.BasketCheckoutQueue, autoAck: true, consumer: consumer, noLocal: false, exclusive: false, arguments: null);
        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == EventBusConstants.BasketCheckoutQueue)
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                var basketCheckoutEvent = JsonConvert.DeserializeObject<BacketCheckoutEvent>(message);

                var command = _mapper.Map<CheckoutOrderCommand>(basketCheckoutEvent);

                var result = await _mediator.Send(command);
            }
        }

        public void Disconnect()
        {
            _connection.Dispose();
        }
    }
}
