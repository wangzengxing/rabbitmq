using RabbitMQ.Client;
using RabbitMQ.Demo.WebApi.RabbitMQ.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.Demo.WebApi.RabbitMQ
{
    public class RabbitMQChannelManager : IDisposable
    {
        private readonly IRabbitMQDescriptorFactory _rabbitMQDescriptorFactory;
        private RabbitMQDescriptor _rabbitMQDescriptor;

        public RabbitMQChannelManager(IRabbitMQDescriptorFactory rabbitMQDescriptorFactory)
        {
            _rabbitMQDescriptorFactory = rabbitMQDescriptorFactory;
        }

        public RabbitMQDescriptor Descriptor
        {
            get
            {
                if (_rabbitMQDescriptor == null)
                {
                    _rabbitMQDescriptor = _rabbitMQDescriptorFactory.CreateDescriptor();
                }

                return _rabbitMQDescriptor;
            }
        }

        public IConnectionFactory ConnectionFactory => Descriptor.ConnectionFactory;

        public IModel Channel => Descriptor.Channel;

        public IConnection Connection => Descriptor.Connection;

        public void Dispose()
        {
            Channel?.Dispose();
            Connection?.Dispose();
        }
    }
}
