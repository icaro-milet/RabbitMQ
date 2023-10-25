using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory()
{
    HostName = "localhost"
};

var connection = factory.CreateConnection();

using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(
        queue: "Teste",
        durable: false,
        exclusive: false,
        autoDelete: false,
        arguments: null);

    var consumer = new EventingBasicConsumer(channel);

    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($" [X] Recebida: {message}");
    };

    channel.BasicConsume(
        queue: "Teste",
        autoAck: true,
        consumer: consumer);

    Console.ReadLine();
}