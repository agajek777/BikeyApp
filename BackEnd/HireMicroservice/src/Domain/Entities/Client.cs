using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Client : BaseEntity
    {
        [Range(0,5)]
        public int NumOfHires { get; set; }

        public virtual ICollection<Hire> Hires { get; set; }
    }
}