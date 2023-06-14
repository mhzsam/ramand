using Application.Services.Interface;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using SharedClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RabbitService : IRabbitService
    {
        private readonly IRabbitMqService _rabbitMqService;
        private readonly RabbitMqConfiguration _configuration;
        public RabbitService(IRabbitMqService rabbitMqService, IOptions<RabbitMqConfiguration> options)
        {
            _rabbitMqService = rabbitMqService;
            _configuration = options.Value;
        }
        public void SendMessage(string channelName, string message)
        {
            var dlxExchangeName = "dlx_exchange";
            string deadChannelName = channelName + "_dead";
            using var connection = _rabbitMqService.CreateChannel();
            using var model = connection.CreateModel();
            model.BasicQos(0, (ushort)_configuration.NumberOfConsumer, false);
            var arguments = new Dictionary<string, object>
                                {
                                    { "x-dead-letter-exchange", "" },
                                    { "x-dead-letter-routing-key", deadChannelName },
                                    { "x-message-ttl", 10000 }
                                };
            model.ExchangeDeclare(dlxExchangeName, ExchangeType.Direct);
            model.QueueDeclare(channelName, true, false, false, arguments);
            model.QueueDeclare(deadChannelName, true, false, false, null);
            model.QueueBind(deadChannelName, dlxExchangeName, "");
            var body = Encoding.UTF8.GetBytes(message);

            model.BasicPublish("", channelName,null, body);
        }
    }
}
