using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

class Program
{
    static void Main(string[] args)
        {
        var senderFactory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "admin",
            Password = "admin",
        };
        using (var senderConnection = senderFactory.CreateConnection())
        using (var senderChannel = senderConnection.CreateModel())
        {
            var message = "Hello, RabbitMQ!";
            var body = Encoding.UTF8.GetBytes(message);

            senderChannel.QueueDeclare(queue: "hello",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            senderChannel.BasicPublish(exchange: "",
                                        routingKey: "hello",
                                        basicProperties: null,
                                        body: body);

            Console.WriteLine(" [x] Sent {0}", message);
        }
    }
}
