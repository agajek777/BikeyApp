using Domain.DTOs;

namespace Application.Common.Interfaces.Communication
{
    public class UserEventMessage : IMessage<UserResponse>
    {
        public string MessageType { get; set; }
        public string Method { get; set; }
        public UserResponse Message { get; set; }
    }
}