using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class HomeBase : BaseEntity
    {
        public int Capacity { get; set; }
        public virtual ICollection<Bike> Bikes { get; set; }
    }
}