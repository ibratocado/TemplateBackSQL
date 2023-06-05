using System.ComponentModel.DataAnnotations;
using System.Security;

namespace ApiTemplate.DTO.Request
{
    public class AccountRequest
    {
        [Required]
        public string? account { get; set; }
        
        [Required]
        public string? pount { get; set; }
    }
}
