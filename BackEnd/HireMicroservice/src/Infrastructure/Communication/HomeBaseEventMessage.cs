using Application.Common.Interfaces.Communication;
using Domain.Dtos;

namespace Infrastructure.Communication
{
    public class HomeBaseEventMessage : IMessage<HomeBaseResponse>
    {
        public string MessageType { get; set; }
        public string Method { get; set; }
        public HomeBaseResponse Message { get; set; }
    }
}