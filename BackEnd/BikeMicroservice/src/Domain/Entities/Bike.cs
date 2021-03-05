using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities
{
    public class Bike : BaseEntity
    {
        [Required]
        public string ModelId { get; set; }
        [ForeignKey(nameof(ModelId))]
        public Model Model { get; set; }

        [DefaultValue(Enums.State.Free)]
        public State State { get; set; }

        public string HomeBaseId { get; set; }
        [ForeignKey(nameof(HomeBaseId))]
        public virtual HomeBase HomeBase { get; set; }

        [Required]
        [DefaultValue(Enums.Size.M)]
        public Size Size { get; set; }
    }
}