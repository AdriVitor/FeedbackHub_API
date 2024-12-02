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

        public RabbitMQService()
        {
            var factory = new ConnectionFactory()
            {
                HostName = hostNameAdress,
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/"
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void ConfigureQueue(string queue, bool durable, bool exclusive, bool autoDelete, string exchange, IDictionary<string, object> arguments = null)
        {
            _channel.QueueDeclare
                (queue: queue,
                 durable: durable,
                 exclusive: exclusive,
                 autoDelete: autoDelete,
                 arguments: arguments);

            _channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Direct, durable:  durable, autoDelete: autoDelete, arguments: arguments);
        }

        public void QueueBind(string queue, string exchange, string routingKey)
        {
            _channel.QueueBind(queue: queue, exchange: exchange, routingKey: routingKey);
        }

        public void SendMessage(string message, string exchange, string routingKey, IBasicProperties basicProperties = null)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: exchange, routingKey: routingKey, basicProperties: basicProperties, body: body);
        }

        public async Task<(string, BasicDeliverEventArgs)> ReceiveMessage(string queue)
        {
            var tcs = new TaskCompletionSource<(string, BasicDeliverEventArgs)>();
            var consumer = new EventingBasicConsumer(_channel);
            string mensagem = string.Empty;
            consumer.Received += async (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                mensagem = Encoding.UTF8.GetString(body);

                tcs.SetResult((mensagem, eventArgs));
            };
            _channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);
            return await tcs.Task;
        }

        public async Task<(string, BasicDeliverEventArgs)> ReceiveMessage(string queue, string correlationId)
        {
            var tcs = new TaskCompletionSource<(string, BasicDeliverEventArgs)>();
            var consumer = new EventingBasicConsumer(_channel);
            string mensagem = string.Empty;
            consumer.Received += async (model, eventArgs) =>
            {
                if (eventArgs.BasicProperties.CorrelationId == correlationId)
                {
                    var body = eventArgs.Body.ToArray();
                    mensagem = Encoding.UTF8.GetString(body);

                    tcs.SetResult((mensagem, eventArgs));
                }
            };
            _channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);
            return await tcs.Task;
        }

        public IBasicProperties CreateBasicProperties()
        {
            return _channel.CreateBasicProperties();
        }

        public IBasicProperties ConfigureBasicProperties(string replyTo)
        {
            var properties = CreateBasicProperties();
            properties.ReplyTo = replyTo;
            properties.CorrelationId = Guid.NewGuid().ToString();

            return properties;
        }

        public void Dispose()
        {
            _connection.Close();
            _channel.Close();
        }
    }
}
