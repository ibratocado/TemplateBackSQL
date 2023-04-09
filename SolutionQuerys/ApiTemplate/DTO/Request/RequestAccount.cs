using System.Security;

namespace ApiTemplate.DTO.Request
{
    public class RequestAccount
    {
        public string? account { get; set; }
        public SecureString? pount { get; set; }
    }
}
