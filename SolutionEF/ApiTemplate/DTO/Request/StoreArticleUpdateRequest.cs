using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.DTO.Request
{
    public class StoreArticleUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid Store { get; set; }
        [Required]
        public Guid Article { get; set; }
    }
}
