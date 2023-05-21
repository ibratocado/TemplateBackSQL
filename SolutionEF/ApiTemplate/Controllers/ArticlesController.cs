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
    [SwaggerTag("CRUD Para Manejo de Articulos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ArticlesController : ControllerBase
    {
        private readonly IGenericCRUD<ArticlesAddRequest, ArticlesUpdateRequest> _articleService;

        public ArticlesController(IGenericCRUD<ArticlesAddRequest, ArticlesUpdateRequest> articleService)
        {
            _articleService = articleService;
        }

        // GET: api/<StoreController>
        [HttpGet("Full")]
        [SwaggerOperation(Summary = "Lista de Articulos",
            Description = "Regresa la lista de Articulos activas")]
        public async Task<ActionResult> GetFull([FromQuery] GenricPaginatorN paginator)
        {
            var respon = await _articleService.GetFull(paginator);
            return Ok(respon);
        }

        // GET api/<StoreController>/5
        [HttpGet("OneById/{id}")]
        [SwaggerOperation(Summary = "Articulo por Id",
            Description = "Regresa una Articulo de acuerdo a la Id")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var respon = await _articleService.GetById(id);
            return Ok(respon);
        }

        // POST api/<StoreController>
        [HttpPost]
        [SwaggerOperation(Summary = "Agregar Articulo",
            Description = "Agrega una Articulo con estado activo")]
        public async Task<ActionResult> Post([FromForm] ArticlesAddRequest model)
        {
            var respon = await _articleService.Add(model);
            return Ok(respon);
        }

        // PUT api/<StoreController>/5
        [HttpPut]
        [SwaggerOperation(Summary = "Actualizar Articulo",
            Description = "Actualiza la Articulo siempre y cuando los datos son correctos ")]
        public async Task<ActionResult> Put([FromForm] ArticlesUpdateRequest model)
        {
            var respon = await _articleService.Update(model);
            return Ok(respon);
        }

        // DELETE api/<StoreController>/5
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Desactivar Articulo",
            Description = "Desactiva el articulo de las peticiones")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var respon = await _articleService.DeleteLogic(id);
            return Ok(respon);
        }
    }
}
