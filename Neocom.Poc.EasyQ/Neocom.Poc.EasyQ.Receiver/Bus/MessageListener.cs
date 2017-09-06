using System;
using System.Diagnostics;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Neocom.Poc.EasyQ.Receiver.Bus
{
    public static class MessageListener
    {
        private static IConnection _connection;
        private static IModel _channel;

        public static void Start()
        {
            var factory = new ConnectionFactory
            {
                HostName = "neogiglocal",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/",
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(15)
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "test-exchange",
                type: "fanout",
                durable: true);

            var queueName = _channel.QueueDeclare().QueueName;

            _channel.QueueBind(queue: queueName,
                exchange: "test-exchange",
                routingKey: "");

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += ConsumerOnReceived;

            _channel.BasicConsume(queue: queueName,
                noAck: true,
                consumer: consumer);
        }

        public static void Stop()
        {
            _channel.Close(200, "Goodbye");
            _connection.Close();
        }

        private static void ConsumerOnReceived(object sender, BasicDeliverEventArgs ea)
        {
            var body = ea.Body;
            var message = Encoding.UTF8.GetString(body);
            Debug.WriteLine("{0}", message);
        }
    }
}