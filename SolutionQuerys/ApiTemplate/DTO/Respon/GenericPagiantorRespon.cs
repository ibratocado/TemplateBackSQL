namespace ApiTemplate.DTO.Respon
{
    public class GenericPagiantorRespon<T> where T : class
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public int RecordsByPage { get; set; }
        public IEnumerable<T>? Data { get; set; }
    }
}
