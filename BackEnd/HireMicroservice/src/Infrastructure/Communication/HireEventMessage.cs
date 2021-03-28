using Application.Common.Interfaces.Communication;
using Domain.Entities;

namespace Infrastructure.Communication
{
    public class HireEventMessage : IMessage<Bike>
    {
        public string MessageType { get; set; }
        public string Method { get; set; }
        public Bike Message { get; set; }
    }
}