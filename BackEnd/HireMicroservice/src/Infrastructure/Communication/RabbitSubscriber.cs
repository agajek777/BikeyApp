using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Communication;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;

namespace Infrastructure.Communication
{
    public class RabbitSubscriber : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AsyncEventingBasicConsumer _consumer;
        private readonly IMapper _mapper;

        public RabbitSubscriber(IServiceProvider serviceProvider, AsyncEventingBasicConsumer consumer, IMapper mapper)
        {
            _serviceProvider = serviceProvider;
            _consumer = consumer;
            _mapper = mapper;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Received += (async (sender, e) =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var decodedMessage = DecodeMessage(e);

                    if (decodedMessage.MessageType == "HomeBaseResponse")
                    {
                        await UpdateHomeBase(scope, e, decodedMessage);
                    }
                    if (decodedMessage.MessageType == "BikeResponse")
                    {
                        await UpdateBike(scope, e, decodedMessage);
                    }
                }
            });
            await Task.CompletedTask;
        }

        private async Task UpdateBike(IServiceScope scope, BasicDeliverEventArgs e, BasicMessage decodedMessage)
        {
            var service = scope.ServiceProvider.GetRequiredService<IBikeService>();

            var decodedBike = DecodeBike(e);

            var bikeResponse = decodedBike.Message;

            var bike = _mapper.Map<BikeResponse, Bike>(bikeResponse);

            switch (decodedMessage.Method)
            {
                case nameof(ApiMethod.POST):
                {
                    await service.AddBikeAsync(bike);
                    break;
                }
                case nameof(ApiMethod.DELETE):
                {
                    await service.DeleteBikeAsync(bike);
                    break;
                }
                case nameof(ApiMethod.PUT):
                {
                    await service.UpdateBikeAsync(bike);
                    break;
                }
            }
        }

        private async Task UpdateHomeBase(IServiceScope scope, BasicDeliverEventArgs e, BasicMessage decodedMessage)
        {
            var service = scope.ServiceProvider.GetRequiredService<IHomeBaseService>();

            var decodedBase = DecodeHomeBase(e);

            var homeBaseResponse = decodedBase.Message;

            var homeBase = _mapper.Map<HomeBaseResponse, HomeBase>(homeBaseResponse);

            switch (decodedMessage.Method)
            {
                case nameof(ApiMethod.POST):
                {
                    await service.AddHomeBaseAsync(homeBase);
                    break;
                }
                case nameof(ApiMethod.DELETE):
                {
                    await service.DeleteHomeBaseAsync(homeBase);
                    break;
                }
                case nameof(ApiMethod.PUT):
                {
                    await service.UpdateHomeBaseAsync(_mapper.Map<HomeBaseUpdateDto>(homeBase));
                    break;
                }
            }
        }

        private BasicMessage DecodeMessage(BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
            
            var message = Encoding.UTF8.GetString(body);
            
            return JsonConvert.DeserializeObject<BasicMessage>(message);
        }
        
        private HomeBaseEventMessage DecodeHomeBase(BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
            
            var message = Encoding.UTF8.GetString(body);
            
            return JsonConvert.DeserializeObject<HomeBaseEventMessage>(message);
        }
        
        private BikeEventMessage DecodeBike(BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
            
            var message = Encoding.UTF8.GetString(body);
            
            return JsonConvert.DeserializeObject<BikeEventMessage>(message);
        }
    }
}