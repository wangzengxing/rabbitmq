using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.Demo.WebApi.RabbitMQ.Abstracts
{
    public interface IRabbitMQDescriptorFactory
    {
        RabbitMQDescriptor CreateDescriptor();

    }
}
