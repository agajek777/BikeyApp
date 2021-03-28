using System.Threading.Tasks;
using Domain.Entities;
using RabbitMQ.Client.Events;

namespace Application.Common.Interfaces.Communication
{
    public interface IEventSubscriber
    {
        Task addHomeBase(object? sender, BasicDeliverEventArgs e);
        void deleteHomeBase(HomeBase homeBase);
        void updateHomeBase(HomeBase homeBase);
    }
}