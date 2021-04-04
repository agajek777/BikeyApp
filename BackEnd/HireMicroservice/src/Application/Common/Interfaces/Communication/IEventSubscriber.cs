using System.Threading.Tasks;
using Domain.Entities;
using RabbitMQ.Client.Events;

namespace Application.Common.Interfaces.Communication
{
    public interface IEventSubscriber
    {
        Task AddHomeBase(object? sender, BasicDeliverEventArgs e);
        void DeleteHomeBase(HomeBase homeBase);
        void UpdateHomeBase(HomeBase homeBase);
    }
}