using EnterpriseService_Application.Services.Interfaces;
using RabbitMQ_Lib;

namespace EnterpriseService_API.BackgroundServices
{
    public class EnterpriseBackgroundService : BackgroundService
    {
        private readonly RabbitMQService _rabbitMQService;
        private IServiceScopeFactory _scopeFactory;
        public EnterpriseBackgroundService(
            RabbitMQService rabbitMQService,
            IServiceScopeFactory scopeFactory
            )
        {
            _rabbitMQService = rabbitMQService;
            _scopeFactory = scopeFactory;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (true)
            {
                await ReceiveMessageEnterpriseExists();
            }
        }

        private async Task ReceiveMessageEnterpriseExists()
        {
            while (true)
            {
                string queueName = "testefila";

                var body = await _rabbitMQService.ReceiveMessage(queueName);
                if (body.Item1 == null) continue;

                using (IServiceScope scope = _scopeFactory.CreateScope())
                {
                    var enterpriseService = scope.ServiceProvider.GetRequiredService<IEnterpriseService>();

                    int enterpriseId = Convert.ToInt32(body.Item1);
                    var enterpriseExists = await enterpriseService.EnterpriseExists(enterpriseId);

                    var replyProperties = _rabbitMQService.CreateBasicProperties();
                    replyProperties.CorrelationId = body.Item2.BasicProperties.CorrelationId;
                    var replyToQueue = body.Item2.BasicProperties.ReplyTo;

                    _rabbitMQService.SendMessage(enterpriseExists.ToString(), "teste2", replyToQueue, replyProperties);
                }
            }
        }
    }
}
