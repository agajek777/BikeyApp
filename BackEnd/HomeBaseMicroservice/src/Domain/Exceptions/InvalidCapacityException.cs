using System;

namespace Domain.Exceptions
{
    public class InvalidCapacityException : Exception
    {
        public InvalidCapacityException(int num) 
            : base($"Invalid capacity {num}. Capacity must be in range <0, 20>.")
        {
        }
    }
}