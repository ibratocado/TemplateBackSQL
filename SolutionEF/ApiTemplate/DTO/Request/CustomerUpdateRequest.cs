using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.DTO.Request
{
    public class CustomerUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? LastNames { get; set; }
        [Required]
        public string? Addres { get; set; }
    }
}
