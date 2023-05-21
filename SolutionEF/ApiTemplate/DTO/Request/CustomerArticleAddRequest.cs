using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.DTO.Request
{
    public class CustomerArticleAddRequest
    {
        [Required]
        public Guid Cuatomer { get; set; }
        [Required]
        public List<Guid> Articles { get; set; }

        public CustomerArticleAddRequest()
        {
            Articles = new List<Guid>();
        }
    }
}
