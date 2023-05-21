using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.DTO.Request
{
    public class StoreArticleAddRequest
    {
        [Required]
        public Guid Store { get; set; }
        [Required]
        public Guid Article { get; set; }
    }
}
