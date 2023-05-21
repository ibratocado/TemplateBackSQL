using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.DTO.Request
{
    public class ArticlesAddRequest
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public int? Stock { get; set; }
    }
}
