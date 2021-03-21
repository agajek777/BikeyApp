using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Client : BaseEntity
    {
        public static int MaxNumOfHires = 5;
        
        [Range(0,5)]
        public int NumOfHires { get; set; }

        public virtual ICollection<Hire> Hires { get; set; }
    }
}