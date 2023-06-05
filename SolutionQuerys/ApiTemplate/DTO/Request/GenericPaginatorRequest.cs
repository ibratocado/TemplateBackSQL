using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.DTO.Request
{
    public class GenericPaginatorRequest<T> where T : class
    {
        public T? Id { get; set; }
        [Required]
        [Range(0,1000)]
        public int Page { get; set; }
        [Required]
        [Range(0,200)]
        public int RecordsByPage { get; set; }
    }
}
