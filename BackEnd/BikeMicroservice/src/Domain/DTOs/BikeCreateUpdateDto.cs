using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities;
using Domain.Enums;

namespace Domain.DTOs
{
    public class BikeCreateUpdateDto
    {
        [Required]
        public string ModelId { get; set; }

        [DefaultValue(Enums.State.Free)]
        public State State { get; set; }

        public string HomeBaseId { get; set; }

        [Required]
        [DefaultValue(Enums.Size.M)]
        public Size Size { get; set; }
    }
}