using Domain.Dtos;

namespace Application.Common.Interfaces.Communication
{
    public class HomeBaseEventMessage : IMessage<HomeBaseResponse>
    {
        public string Method { get; set; }
        public HomeBaseResponse Message { get; set; }
    }
}