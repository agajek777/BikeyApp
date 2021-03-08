using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Communication;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Persistence;
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
                    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                
                    var body = e.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var homebaseMessage = JsonConvert.DeserializeObject<HomeBasePostMessage>(message);

                    var homeBase = homebaseMessage.Message;
                
                    Console.WriteLine(message);
                    db.HomeBases.Add(_mapper.Map<HomeBaseResponse, HomeBase>(homeBase));

                    await db.SaveChangesAsync();
                }
            });
            await Task.CompletedTask;
        }
    }
}