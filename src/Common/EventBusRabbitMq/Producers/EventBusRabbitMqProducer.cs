using EventBusRabbitMq.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace EventBusRabbitMq.Producers
{
    public class EventBusRabbitMqProducer
    {
        private IRabbitMqConnection _connection;

        public EventBusRabbitMqProducer(IRabbitMqConnection connection)
        {
            _connection = connection;
        }

        public void PublishBasketCheckout(string queueName, BacketCheckoutEvent publishModel)
        {
            using (var channel = _connection.CreateModel())
            {
                // declare queue and  encoding the model
                channel.QueueDeclare(queueName, false, false, false, null);
                var message = JsonConvert.SerializeObject(publishModel);
                var body = Encoding.UTF8.GetBytes(message);

                IBasicProperties properties = channel.CreateBasicProperties();

                properties.Persistent = true;
                properties.DeliveryMode = 2;

                // publish
                channel.ConfirmSelect();
                channel.BasicPublish(exchange: "", routingKey: queueName, mandatory: true, basicProperties: properties, body: body);
                channel.WaitForConfirmsOrDie();

                //after publish ack
                channel.BasicAcks += (sender, eventArgs) =>
                {
                    Console.WriteLine("Sent RabbitMq");
                };

                channel.ConfirmSelect();

            }
        }
    }
}
