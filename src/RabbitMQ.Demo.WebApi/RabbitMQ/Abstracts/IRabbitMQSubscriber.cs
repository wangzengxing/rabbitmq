using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.Demo.WebApi.RabbitMQ.Abstracts
{
    public interface IRabbitMQSubscriber
    {
        void SubscribeWorkQueue<TMessage>(Action<TMessage> action, string queue);
        void Subscribe<TMessage>(Action<TMessage> subscribeDelegate, string exchange);
        void SubscribeWithRoutingKey<TMessage>(Action<TMessage> subscribeDelegate, string exchange, IEnumerable<string> routingKeys);
        void SubscribeWithTopic<TMessage>(Action<TMessage> subscribeDelegate, string exchange, IEnumerable<string> routingKeys);
    }
}
