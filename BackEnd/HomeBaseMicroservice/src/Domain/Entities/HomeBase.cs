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
        public Capacity Capacity { get; set; }

        [Required]
        [DefaultValue(52.217)]
        public CoordinateLat CoordinateLat { get; set; }

        [Required]
        [DefaultValue(21.0)]
        public CoordinateLon CoordinateLon { get; set; }
    }
}