//connectionFactory connection channel
using System.Text;
using RabbitMQ.Client;

var connectionFactory = new ConnectionFactory()
{
    HostName = "localhost"
};

var connection = connectionFactory.CreateConnection();

var channel = connection.CreateModel();

// channel.QueueDeclare(queue: "Hello", durable: false, exclusive: false, autoDelete: false, arguments: null);
channel.ExchangeDeclare(exchange:"exmsg",ExchangeType.Fanout);
var message = "Hello RabbitMQ";
var messageBytes = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(exchange: "exmsg" /*exchange: ""*/, routingKey: string.Empty /*routingKey: "Hello"*/, basicProperties: null, body: messageBytes);

System.Console.WriteLine("Mesaj Gönderildi");

Console.ReadKey();