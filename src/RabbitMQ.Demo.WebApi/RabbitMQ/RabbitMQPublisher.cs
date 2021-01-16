using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Demo.WebApi.RabbitMQ.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Demo.WebApi.RabbitMQ
{
    public class RabbitMQPublisher : RabbitMQChannelManager, IRabbitMQPublisher, IDisposable
    {
        public RabbitMQPublisher(IRabbitMQDescriptorFactory rabbitMQDescriptorFactory)
            : base(rabbitMQDescriptorFactory)
        {

        }

        public void PublishWorkQueue<TMessage>(TMessage message, string queue)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (string.IsNullOrEmpty(queue))
            {
                throw new ArgumentNullException(nameof(queue));
            }

            Channel.QueueDeclare(queue: queue,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var properties = Channel.CreateBasicProperties();
            properties.Persistent = true;

            var messageJson = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(messageJson);

            Channel.BasicPublish("", queue, false, properties, body);
        }

        public void Publish<TMessage>(TMessage message, string exchange)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (string.IsNullOrEmpty(exchange))
            {
                throw new ArgumentNullException(nameof(exchange));
            }

            Channel.ExchangeDeclare(exchange: exchange, type: "fanout");

            var messageJson = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(messageJson);

            Channel.BasicPublish(exchange: exchange, routingKey: "", false, null, body);
        }

        public void PublishWithRoutingKey<TMessage>(TMessage message, string exchange, string routingKey)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (string.IsNullOrEmpty(exchange))
            {
                throw new ArgumentNullException(nameof(exchange));
            }

            if (string.IsNullOrEmpty(routingKey))
            {
                throw new ArgumentNullException(nameof(routingKey));
            }

            Channel.ExchangeDeclare(exchange: exchange, type: "direct");

            var messageJson = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(messageJson);

            Channel.BasicPublish(exchange: exchange, routingKey: routingKey, false, null, body);
        }

        public void PublishWithTopic<TMessage>(TMessage message, string exchange, string routingKey)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (string.IsNullOrEmpty(exchange))
            {
                throw new ArgumentNullException(nameof(exchange));
            }

            if (string.IsNullOrEmpty(routingKey))
            {
                throw new ArgumentNullException(nameof(routingKey));
            }

            Channel.ExchangeDeclare(exchange: exchange, type: "topic");

            var messageJson = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(messageJson);

            Channel.BasicPublish(exchange: exchange, routingKey: routingKey, false, null, body);
        }
    }
}
