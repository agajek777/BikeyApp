using System;

namespace Domain.Entities
{
    public class BaseEntity
    {
        public string Id { get; set; }
        
        public DateTime DateCreated { get; set; }
        
        public DateTime DateModified { get; set; }
    }
}