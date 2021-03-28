namespace Application.Common.Interfaces.Communication
{
    public interface IMessage<T>
    {
        public string MessageType { get; set; }
        public string Method { get; set; }
        public T Message { get; set; }
    }
}