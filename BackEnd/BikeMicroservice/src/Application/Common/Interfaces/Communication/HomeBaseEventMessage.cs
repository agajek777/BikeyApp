using Domain.DTOs;
using Domain.Entities;

namespace Application.Common.Interfaces.Communication
{
    public class HomeBaseEventMessage : IMessage<HomeBaseResponse>
    {
        public string MessageType { get; set; }
        public string Method { get; set; }
        public HomeBaseResponse Message { get; set; }
    }
}