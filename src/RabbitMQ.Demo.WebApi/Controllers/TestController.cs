using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Demo.WebApi.Model;
using RabbitMQ.Demo.WebApi.RabbitMQ.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQ.Demo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IRabbitMQPublisher _publisher;
        private readonly IRabbitMQSubscriber _subscriber;

        public TestController(IRabbitMQPublisher publisher, IRabbitMQSubscriber subscriber)
        {
            _publisher = publisher;
            _subscriber = subscriber;
        }

        [HttpGet("send")]
        public void Send([FromQuery] string level)
        {
            _publisher.PublishWithTopic(new PersonMessage
            {
                Id = 1,
                Age = 25,
                Name = "jack"
            }, "test2", level);
        }

        [HttpGet("Receive")]
        public void Receive()
        {
            _subscriber.SubscribeWithTopic<PersonMessage>(message =>
            {
                Console.WriteLine(message);
            }, "test2", new string[] { "info", "debug", "error" });
        }
    }
}
