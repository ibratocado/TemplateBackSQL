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
    [SwaggerTag("CRUD Para Manejo de Articulos Asociados a Tienda")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StoreArticleController : ControllerBase
    {
        private readonly IGenericCRUD<StoreArticleAddRequest, StoreArticleUpdateRequest> _storeArticleService;

        public StoreArticleController(IGenericCRUD<StoreArticleAddRequest, StoreArticleUpdateRequest> storeArticleService)
        {
            _storeArticleService = storeArticleService;
        }

        [HttpGet("ById")]
        [SwaggerOperation(Summary = "Articulos por Id de Tienda",
            Description = "Regresa los articulos Asociados a una Tienda")]
        public async Task<ActionResult> GetById([FromQuery] GenericPaginatorRequest paginator)
        {
            var respon = await _storeArticleService.GetById(paginator.Id, paginator);
            return Ok(respon);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Agregar Articulo de cliente",
            Description = "Agrega una tanda de articulos para un cliente")]
        public async Task<ActionResult> Post([FromBody] StoreArticleAddRequest model)
        {
            var respon = await _storeArticleService.Add(model);
            return Ok(respon);
        }
    }
}
