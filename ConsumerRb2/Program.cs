using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var connectionFactory = new ConnectionFactory();
connectionFactory.HostName = "localhost";
var connection = connectionFactory.CreateConnection();
var channel = connection.CreateModel();

var queueName = channel.QueueDeclare().QueueName;
channel.QueueBind(queue: queueName, exchange: "exmsg", routingKey: string.Empty);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    var byteMessage = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(byteMessage);
    channel.BasicAck(ea.DeliveryTag, multiple: false);
    System.Console.WriteLine("Mesaj geldi2: " + message);
};
channel.BasicConsume(queueName/*queue: "Hello"*/ , autoAck: false, consumer: consumer);

Console.ReadKey();