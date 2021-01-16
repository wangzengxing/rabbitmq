using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Demo.WebApi.RabbitMQ.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Demo.WebApi.RabbitMQ
{
    public class RabbitMQSubscriber : RabbitMQChannelManager, IRabbitMQSubscriber, IDisposable
    {
        public RabbitMQSubscriber(IRabbitMQDescriptorFactory rabbitMQDescriptorFactory)
            : base(rabbitMQDescriptorFactory)
        {
        }

        public void SubscribeWorkQueue<TMessage>(Action<TMessage> subscribeDelegate, string queue)
        {
            Channel.QueueDeclare(queue: queue,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            Channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += (model, args) =>
            {
                var body = args.Body;
                var messageJson = Encoding.UTF8.GetString(body.ToArray());

                var message = JsonConvert.DeserializeObject<TMessage>(messageJson);
                subscribeDelegate?.Invoke(message);

                Channel.BasicAck(args.DeliveryTag, false);
            };
            Channel.BasicConsume(queue: queue, autoAck: false, consumer: consumer);
        }

        public void Subscribe<TMessage>(Action<TMessage> subscribeDelegate, string exchange)
        {
            Channel.ExchangeDeclare(exchange: exchange, type: "fanout");

            var queueName = Channel.QueueDeclare().QueueName;
            Channel.QueueBind(queue: queueName,
                              exchange: exchange,
                              routingKey: "");

            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += (model, args) =>
            {
                var body = args.Body;
                var messageJson = Encoding.UTF8.GetString(body.ToArray());

                var message = JsonConvert.DeserializeObject<TMessage>(messageJson);
                subscribeDelegate?.Invoke(message);
            };
            Channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }

        public void SubscribeWithRoutingKey<TMessage>(Action<TMessage> subscribeDelegate, string exchange, IEnumerable<string> routingKeys)
        {
            Channel.ExchangeDeclare(exchange: exchange,
                                    type: "direct");
            var queueName = Channel.QueueDeclare().QueueName;

            foreach (var routingKey in routingKeys)
            {
                Channel.QueueBind(queue: queueName,
                                  exchange: exchange,
                                  routingKey: routingKey);
            }

            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var messageJson = Encoding.UTF8.GetString(body.ToArray());

                var message = JsonConvert.DeserializeObject<TMessage>(messageJson);
                subscribeDelegate?.Invoke(message);
            };
            Channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);
        }

        public void SubscribeWithTopic<TMessage>(Action<TMessage> subscribeDelegate, string exchange, IEnumerable<string> routingKeys)
        {
            Channel.ExchangeDeclare(exchange: exchange,
                                    type: "topic");
            var queueName = Channel.QueueDeclare().QueueName;

            foreach (var routingKey in routingKeys)
            {
                Channel.QueueBind(queue: queueName,
                                  exchange: exchange,
                                  routingKey: routingKey);
            }

            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var messageJson = Encoding.UTF8.GetString(body.ToArray());

                var message = JsonConvert.DeserializeObject<TMessage>(messageJson);
                subscribeDelegate?.Invoke(message);
            };
            Channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);
        }
    }
}
