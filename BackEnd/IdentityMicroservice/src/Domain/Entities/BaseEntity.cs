using System;

namespace Domain.Entities
{
    public interface IBaseEntity
    {
        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}