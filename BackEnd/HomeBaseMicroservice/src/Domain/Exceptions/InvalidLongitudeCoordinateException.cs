using System;
using ValueOf;

namespace Domain.Exceptions
{
    public class InvalidLongitudeCoordinateException : Exception
    {
        public InvalidLongitudeCoordinateException(double value)
            :base($"Invalid longitude coordinate {value}. Value must be between 14deg 07'E and 24deg 09'E")
        {
        }
    }
}