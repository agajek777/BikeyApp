using Domain.Exceptions;
using ValueOf;

namespace Domain.ValueObjects
{
    public class Capacity : ValueOf<int, Capacity>
    {
        private const int MinCapacity = 0;
        private const int MaxCapacity = 20;

        protected override void Validate()
        {
            if (Value is < MinCapacity or > MaxCapacity)
                throw new InvalidCapacityException(Value);
        }
    }
}