﻿using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.DTO.Request
{
    public class StoreUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? Branch { get; set; }
        [Required]
        public string? Addres { get; set; }
    }
}
