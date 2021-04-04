using Application.Common.Interfaces.Communication;
using Domain.Dtos;

namespace Infrastructure.Communication
{
    public class ClientEventMessage : IMessage<UserResponse>
    {
        public string MessageType { get; set; }
        public string Method { get; set; }
        public UserResponse Message { get; set; }
    }
}