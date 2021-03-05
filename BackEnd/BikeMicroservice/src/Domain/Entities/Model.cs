using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities
{
    public class Model : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        [DefaultValue(Type.City)]
        public Type Type { get; set; }

        [Required]
        public bool Shocks { get; set; }

        [Required]
        [Range(16, 31)]
        public int Gears { get; set; }

        [Required]
        [Range(16.0, 30.0)]
        public double WheelSize { get; set; }
    }
}