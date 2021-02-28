using System;

namespace Domain.Exceptions
{
    public class InvalidLatitudeCoordinateException : Exception
    {
        public InvalidLatitudeCoordinateException(double value) 
            :base($"Invalid latitude coordinate {value}. Value must be between 49deg 0'N and 54deg 50'N")
        {
            
        }
    }
}