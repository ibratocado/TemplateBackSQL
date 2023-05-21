using ApiTemplate.DTO.Request;
using ApiTemplate.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiTemplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("CRUD Para Manejo de Articulos Asociados a Cliente")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomerArticleController : ControllerBase
    {
        private readonly IGenericCRUD<CustomerArticleAddRequest, CustomerArticleUpdateRequest> _customerArticleService;

        public CustomerArticleController(IGenericCRUD<CustomerArticleAddRequest, CustomerArticleUpdateRequest> customerArticleService)
        {
            _customerArticleService = customerArticleService;
        }

        [HttpGet("ById/{id}")]
        [SwaggerOperation(Summary = "Articulos por Id de Cliente",
            Description = "Regresa los articulos Asociados a un cliente")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var respon = await _customerArticleService.GetById(id);
            return Ok(respon);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Agregar Articulo de cliente",
            Description = "Agrega una tanda de articulos para un cliente")]
        public async Task<ActionResult> Post([FromBody] CustomerArticleAddRequest model)
        {
            var respon = await _customerArticleService.Add(model);
            return Ok(respon);
        }
    }
}
