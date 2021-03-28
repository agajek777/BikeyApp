using Domain.DTOs;

namespace Application.Common.Interfaces.Communication
{
    public class BikeEventMessage : IMessage<BikeResponse>
    {
        public string MessageType { get; set; }
        public string Method { get; set; }
        public BikeResponse Message { get; set; }
    }
}