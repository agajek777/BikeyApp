using Domain.DTOs;
using Domain.Entities;

namespace Application.Common.Interfaces.Communication
{
    public class HomeBasePostMessage : IMessage<HomeBaseResponse>
    {
        public string Method { get; set; }
        public HomeBaseResponse Message { get; set; }
    }
}