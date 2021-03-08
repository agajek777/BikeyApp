using Application.Common.Enums;

namespace Application.Common.Interfaces.Communication
{
    public interface IEventPublisher
    {
        public void PublishEvent(IMessage message);
    }
}