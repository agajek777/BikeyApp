using System;
using Domain.Enums;

namespace Domain.Dtos
{
    public class HireResponse
    {
        public string Id { get; set; }
        
        public DateTime DateCreated { get; set; }
        
        public DateTime DateModified { get; set; }
        
        public HireState State { get; set; }

        public string ClientId { get; set; }
        
        public string BikeId { get; set; }
    }
}