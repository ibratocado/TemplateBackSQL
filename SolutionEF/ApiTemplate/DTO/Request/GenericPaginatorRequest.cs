using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.DTO.Request
{
    public class GenericPaginatorRequest<T> where T : class
    {
        public T? Id { get; set; }
        [Required]
        public int Page { get; set; }
        [Required]
        public int RecordsByPage { get; set; }
    }
}
