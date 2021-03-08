using Application.Common.Enums;

namespace Application.Common.Interfaces.Communication
{
    public class HomeBasePostMessage : IMessage
    {
        public string Method { get; set; }
        public object Message { get; set; }
    }
}