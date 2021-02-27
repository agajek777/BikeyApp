using Domain.Exceptions;
using ValueOf;

namespace Domain.ValueObjects
{
    public class CoordinateLon : ValueOf<double, CoordinateLon>
    {
        protected override void Validate()
            {
                if (Value is < 14.117 or > 24.15)
                    throw new InvalidLongitudeCoordinate(Value);
            }
        }
}