using Domain.Entities;

namespace Application.Common.Interfaces.Communication
{
    public class HireEventMessage : IMessage<Bike>
    {
        public string MessageType { get; set; }
        public string Method { get; set; }
        public Bike Message { get; set; }
    }
}