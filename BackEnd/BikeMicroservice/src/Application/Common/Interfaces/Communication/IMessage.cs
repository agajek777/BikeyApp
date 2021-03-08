namespace Application.Common.Interfaces.Communication
{
    public interface IMessage<T>
    {
        public string Method { get; set; }
        public T Message { get; set; }
    }
}