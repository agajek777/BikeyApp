using Application.Common.Interfaces.Communication;
using Domain.Dtos;

namespace Infrastructure.Communication
{
    public class BikeEventMessage : IMessage<BikeResponse>
    {
        public string MessageType { get; set; }
        public string Method { get; set; }
        public BikeResponse Message { get; set; }
    }
}