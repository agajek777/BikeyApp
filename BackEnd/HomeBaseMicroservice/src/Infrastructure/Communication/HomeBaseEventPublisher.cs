using System.Text;
using Application.Common.Enums;
using Application.Common.Interfaces.Communication;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Infrastructure.Communication
{
    public class HomeBaseEventPublisher : IEventPublisher
    {
        private readonly IModel _client;

        public HomeBaseEventPublisher(IModel client)
        {
            _client = client;
        }

        public void PublishEvent(IMessage message)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            
            _client.BasicPublish("", "homebases-queue", null, body);
        }
    }
}