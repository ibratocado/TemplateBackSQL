using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.DTO.Request
{
    public class CustomerArticleUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid Cuatomer { get; set; }
        [Required]
        public Guid Article { get; set; }
    }
}
