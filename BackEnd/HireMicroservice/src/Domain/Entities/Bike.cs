using Domain.Enums;

namespace Domain.Entities
{
    public class Bike : BaseEntity
    {
        public State State { get; set; }
    }
}