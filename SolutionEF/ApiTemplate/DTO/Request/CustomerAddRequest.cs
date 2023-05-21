using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.DTO.Request
{
    public class CustomerAddRequest
    {
        [Required]
        public Guid? Account { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? LastNames { get; set; }
        [Required]
        public string? Addres { get; set; }
    }
}
