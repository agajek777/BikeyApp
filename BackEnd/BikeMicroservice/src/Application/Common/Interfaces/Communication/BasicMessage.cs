namespace Application.Common.Interfaces.Communication
{
    public class BasicMessage : IMessage<object>
    {
        public string MessageType { get; set; }
        public string Method { get; set; }
        public object Message { get; set; }
    }
}