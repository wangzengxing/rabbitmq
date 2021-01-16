using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.Demo.WebApi.RabbitMQ.Abstracts
{
    public interface IRabbitMQPublisher
    {
        void PublishWorkQueue<TMessage>(TMessage message, string queue);
        void Publish<TMessage>(TMessage message, string exchange);
        void PublishWithRoutingKey<TMessage>(TMessage message, string exchange, string routingKey);
        void PublishWithTopic<TMessage>(TMessage message, string exchange, string routingKey);
    }
}
