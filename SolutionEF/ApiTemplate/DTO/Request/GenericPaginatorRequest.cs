namespace ApiTemplate.DTO.Request
{
    public class GenericPaginatorRequest
    {
        public Guid Id { get; set; }
        public int Page { get; set; }
        public int RecordsByPage { get; set; }
    }
}
