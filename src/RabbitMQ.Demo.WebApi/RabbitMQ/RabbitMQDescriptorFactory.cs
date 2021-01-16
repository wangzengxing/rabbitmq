using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Demo.WebApi.RabbitMQ.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.Demo.WebApi.RabbitMQ
{
    public class RabbitMQDescriptorFactory : IRabbitMQDescriptorFactory
    {
        private readonly RabbitMQOptions _options;

        public RabbitMQDescriptorFactory(IOptions<RabbitMQOptions> optionsAccesser)
        {
            _options = optionsAccesser.Value;
        }

        public RabbitMQDescriptor CreateDescriptor()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = _options.HostName,
                Port = _options.Port,
                UserName = _options.UserName,
                Password = _options.Password
            };

            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();

            var descriptor = new RabbitMQDescriptor(connectionFactory, connection, channel);
            return descriptor;
        }
    }
}
