﻿using System.Text;
using Application.Common.Enums;
using Application.Common.Interfaces.Communication;
using Domain.DTOs;
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

        public void PublishEvent(IMessage<HomeBaseResponse> message)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            
            _client.BasicPublish("amq.topic", "homebase", null, body);
        }
    }
}