using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Communication;
using Infrastructure.Communication;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "WebUI", Version = "v1"}); });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            var assembly = AppDomain.CurrentDomain.Load("Application");
            services.AddMediatR(assembly);
            services.AddAutoMapper(assembly);

            services.AddScoped<IHomeBaseService, HomeBaseService>();
            
            var factory = new ConnectionFactory
            {
                DispatchConsumersAsync = true,
                Uri = new Uri("amqps://kmvlcpdx:L0Nh4_djiqbyHZV1AxeQBAgq2b5Q_0dB@sparrow.rmq.cloudamqp.com/kmvlcpdx")
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare("homebase-hire-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            channel.QueueDeclare("bike-hire-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            channel.QueueDeclare("hire-bike-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            channel.BasicConsume("homebase-hire-queue", true, consumer);
            channel.BasicConsume("bike-hire-queue", true, consumer);
            channel.BasicConsume("identity-hire-queue", true, consumer);
            services.AddSingleton<AsyncEventingBasicConsumer>(consumer);
            services.AddHostedService<RabbitSubscriber>();
            services.AddSingleton(channel);
            services.AddScoped<IEventPublisher, BikeEventPublisher>();

            services.AddScoped<IBikeService, BikeService>();
            services.AddScoped<IClientService, ClientService>();
            
            services.AddScoped<IHireRepository, HireRepository>();
            services.AddScoped<IHireService, HireService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebUI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}