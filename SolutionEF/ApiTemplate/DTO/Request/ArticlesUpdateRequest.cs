using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.DTO.Request
{
    public class ArticlesUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public IFormFile? Image { get; set; }
        [Required]
        public int? Stock { get; set; }
    }
}
