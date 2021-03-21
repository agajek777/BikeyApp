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
    public class HomeBaseSubscriber : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AsyncEventingBasicConsumer _consumer;
        private readonly IMapper _mapper;

        public HomeBaseSubscriber(IServiceProvider serviceProvider, AsyncEventingBasicConsumer consumer, IMapper mapper)
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
                    var service = scope.ServiceProvider.GetRequiredService<IHomeBaseService>();

                    var homebaseMessage = DecodeMessage(e);

                    var homeBaseResponse = homebaseMessage.Message;
                    
                    var homeBase = _mapper.Map<HomeBaseResponse, HomeBase>(homeBaseResponse);

                    switch (homebaseMessage.Method)
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
            });
            await Task.CompletedTask;
        }
        
        private HomeBaseEventMessage DecodeMessage(BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
            
            var message = Encoding.UTF8.GetString(body);
            
            return JsonConvert.DeserializeObject<HomeBaseEventMessage>(message);
        }
    }
}