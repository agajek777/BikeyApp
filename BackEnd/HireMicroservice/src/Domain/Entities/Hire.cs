using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Hire : BaseEntity
    {
        [Required]
        public string ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        public virtual Client Client { get; set; }
        
        [Required] 
        public string BikeId { get; set; }
        [ForeignKey(nameof(BikeId))]
        public virtual Bike Bike { get; set; }
    }
}