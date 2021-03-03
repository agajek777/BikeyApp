using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class HomeBase : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, 20)]
        public int Capacity { get; set; }

        [Required]
        [DefaultValue(52.217)]
        [Range(49.0, 54.5)]
        public double CoordinateLat { get; set; }

        [Required]
        [DefaultValue(21.0)]
        [Range(14.117, 24.15)]
        public double CoordinateLon { get; set; }
    }
}