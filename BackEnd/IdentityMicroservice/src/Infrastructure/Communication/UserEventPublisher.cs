using System.Text;
using Application.Common.Interfaces.Communication;
using Domain.DTOs;
using Domain.Entities;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Infrastructure.Communication
{
    public class UserEventPublisher : IEventPublisher
    {
        private readonly IModel _client;
        
        public UserEventPublisher(IModel client)
        {
            _client = client;
        }
        
        public void PublishEvent(IMessage<UserResponse> message)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            
            _client.BasicPublish("amq.topic", "identity", null, body);
        }
    }
}