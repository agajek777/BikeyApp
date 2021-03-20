using Domain.Enums;

namespace Domain.Dtos
{
    public class HireForCreateUpdateDto
    {
        public string ClientId { get; set; }
        
        public string BikeId { get; set; }
        
        public HireState State { get; set; }
    }
}