﻿using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using EasyNetQ;
using EasyNetQ.NonGeneric;
using EasyNetQ.Topology;
using Neocom.Poc.EasyQ.App_Start;
using Neocom.Poc.EasyQ.Model;
using RabbitMQ.Client;

namespace Neocom.Poc.EasyQ.Controller
{
    [RoutePrefix("api/send")]
    public class SendController : ApiController
    {
        private readonly IBus _bus;

        public SendController(IBus bus)
        {
            _bus = bus;
        }

        [Route("client")]
        [HttpGet]
        public async Task<HttpResponseMessage> BuildMessageWithRabbitClient()
        {
            

            var msg = new {Text = "I'm here motherfuck!"};
            var msg2 = new EasyNetQ.Message<dynamic>(msg);

            _bus.Publish(new Message
            {
                Text = "I'm here motherfuck!"
            });



            var factory = new ConnectionFactory
            {
                HostName = "neogiglocal",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "test-exchange",
                    type: "fanout",
                    durable: true);

                var message = "Current time: " + DateTime.Now.ToLongTimeString();
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "test-exchange",
                    routingKey: "",
                    basicProperties: null,
                    body: body);
            }

            var result = new StringContent("All is good".SerializeToJsonLowerCase(), Encoding.UTF8, "application/json");
            var response = new HttpResponseMessage
            {
                Content = result,
                StatusCode = HttpStatusCode.OK
            };

            return response;

        }

        //[Route("")]
        //[HttpGet]
        //public async Task<HttpResponseMessage> BuildMessage()
        //{
        //    var msg = new Message {Value = "Hello Rabbit!"};

        //    var serviceBus = RabbitHutch.CreateBus("host=localhost;virtualHost=/;username=guest;password=guest");
        //    var adBus = RabbitHutch.CreateBus("host=localhost;virtualHost=/;username=guest;password=guest").Advanced;
        //    adBus.QueueDeclare("my_queue");

        //    var properties = new MessageProperties();
        //    var body = Encoding.UTF8.GetBytes("Hello World!");

        //    var myMessage = new EasyNetQ.Message<Neocom.Poc.EasyQ.Model.Message>(msg);

        //    serviceBus.Publish(Exchange.GetDefault(), "my_queue");
            

        //    //serviceBus.Publish(msg);

        //    await serviceBus.PublishAsync(new Message
        //    {
        //        Value = "Hello Rabbit!"
        //    }).ContinueWith(task =>
        //    {
        //        if (task.IsCompleted)
        //            Console.Out.WriteLine("{0} Completed", 0);

        //        if (task.IsFaulted)
        //        {
        //            Console.Out.WriteLine("\n\n");
        //            Console.Out.WriteLine(task.Exception);
        //            Console.Out.WriteLine("\n\n");
        //        }
        //    });

        //    var result = new StringContent("All is good".SerializeToJsonLowerCase(), Encoding.UTF8, "application/json");
        //    var response = new HttpResponseMessage
        //    {
        //        Content = result,
        //        StatusCode = HttpStatusCode.OK
        //    };

        //    return response;
        //}
    }
}