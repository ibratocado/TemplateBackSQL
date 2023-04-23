using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.DTO.Request
{
    public class ArticlesPostRequest
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public float? Price { get; set; }
    }
}
