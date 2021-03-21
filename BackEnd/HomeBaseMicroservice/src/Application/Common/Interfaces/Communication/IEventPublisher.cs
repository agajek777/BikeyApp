using Application.Common.Enums;
using Domain.DTOs;

namespace Application.Common.Interfaces.Communication
{
    public interface IEventPublisher
    {
        public void PublishEvent(IMessage<HomeBaseResponse> message);
    }
}