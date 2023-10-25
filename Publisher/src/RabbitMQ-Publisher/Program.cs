using RabbitMQ.Client;
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

    string message = "Bem-vindo ao RabbitMQ";

    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(
        exchange: "",
        routingKey: "Teste",
        basicProperties: null,
        body: body);

    Console.WriteLine($" [X] Enviada: {message}");
}