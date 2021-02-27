using Domain.Exceptions;
using ValueOf;

namespace Domain.ValueObjects
{
    public class CoordinateLat : ValueOf<double, CoordinateLat>
    {
        protected override void Validate()
        {
            if (Value is < 49.0 or > 54.5)
                throw new InvalidLatitudeCoordinate(Value);
        }
    }
}