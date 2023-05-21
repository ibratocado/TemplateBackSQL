﻿using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.DTO.Request
{
    public class StoreAddRequest
    {
        [Required]
        public string? Branch { get; set; }
        [Required]
        public string? Addres { get; set; }
    }
}
