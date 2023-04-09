using Swashbuckle.AspNetCore.Annotations;

namespace ApiTemplate.DTO.Respon
{
    public class GenericRespon
    {
        [SwaggerSchema(Description = "Estado de la peticion")]
        public int State { get; set; }

        [SwaggerSchema(Description = "Mensage de lo ocurrido")]
        public string? Message { get; set; }

        [SwaggerSchema(Description = "Datos o descricion de lo ocurrido")]
        public object? Data { get; set; }
    }
}
