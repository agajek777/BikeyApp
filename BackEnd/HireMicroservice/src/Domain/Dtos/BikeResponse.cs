﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Dtos
{
    public class BikeResponse
    {
        public string Id { get; set; }
        
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