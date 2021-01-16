using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.Demo.WebApi.RabbitMQ.Abstracts
{
    public class RabbitMQDescriptor
    {
        public RabbitMQDescriptor(IConnectionFactory connectionFactory, IConnection connection, IModel channel)
        {
            ConnectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            Channel = channel ?? throw new ArgumentNullException(nameof(channel));
        }

        public IConnectionFactory ConnectionFactory { get; }
        public IConnection Connection { get; }
        public IModel Channel { get; }
    }
}
