﻿using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.DTO.Request
{
    public class ArticlesPutRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public float? Price { get; set; }
    }
}
