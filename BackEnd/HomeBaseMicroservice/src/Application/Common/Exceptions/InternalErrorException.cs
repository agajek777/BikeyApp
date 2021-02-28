using System;

namespace Application.Common.Exceptions
{
    public class InternalErrorException : Exception
    {
        public InternalErrorException(string message)
            :base(message)
        {
        }
    }
}