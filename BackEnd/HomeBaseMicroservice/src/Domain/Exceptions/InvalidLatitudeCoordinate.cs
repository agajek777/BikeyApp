using System;

namespace Domain.Exceptions
{
    public class InvalidLatitudeCoordinate : Exception
    {
        public InvalidLatitudeCoordinate(double value) 
            :base($"Invalid latitude coordinate {value}. Value must be between 49deg 0'N and 54deg 50'N")
        {
            
        }
    }
}