using System.Text;
using Application.Common.Interfaces.Communication;
using Domain.DTOs;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Infrastructure.Communication
{
    public class BikeEventPublisher : IEventPublisher
    {
        private readonly IModel _client;

        public BikeEventPublisher(IModel client)
        {
            _client = client;
        }
        
        public void PublishEvent(IMessage<BikeResponse> message)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            
            _client.BasicPublish("amq.topic", "bike", null, body);
        }
    }
}