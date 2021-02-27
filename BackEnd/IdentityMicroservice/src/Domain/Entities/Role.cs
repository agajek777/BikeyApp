using System;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class Role : IdentityRole, IBaseEntity
    {
        public DateTime DateCreated { get; set; }
        
        public DateTime DateModified { get; set; }
    }
}