using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ_Lib
{
    public class RabbitMQService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string hostNameAdress = "localhost";

        public RabbitMQService(string queue, bool durable = false, bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null)
        {
            var factory = new ConnectionFactory() { HostName = hostNameAdress };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare
                (queue: queue, 
                 durable: durable, 
                 exclusive: exclusive, 
                 autoDelete: autoDelete, 
                 arguments: arguments);
        }

        public void SendMessage(string message, string exchange, string routingKey, IBasicProperties basicProperties = null)
        {
            var body = Encoding.UTF8.GetBytes(message);
            
            _channel.BasicPublish(exchange: exchange, routingKey: routingKey, basicProperties: basicProperties, body: body);
        }

        public void ReceiveMessage(string queue) {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
            };

            _channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);
        }

        public void Dispose()
        {
            _connection.Close();
            _channel.Close();
        }
    }
}
