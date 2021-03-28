using Domain.Dtos;
using Domain.Entities;

namespace Application.Common.Interfaces.Communication
{
    public interface IEventPublisher
    {
        public void PublishEvent(IMessage<Bike> message);
    }
}