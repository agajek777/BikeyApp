using Domain.DTOs;

namespace Application.Common.Interfaces.Communication
{
    public interface IEventPublisher
    {
        public void PublishEvent(IMessage<BikeResponse> message);
    }
}