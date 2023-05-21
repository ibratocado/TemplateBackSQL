using ApiTemplate.DTO.Request;
using ApiTemplate.DTO.Respon;
using ApiTemplate.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTemplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("CRUD Para Manejo de Tiendas")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StoreController : ControllerBase
    {
        private readonly IGenericCRUD<StoreAddRequest,StoreUpdateRequest> _storeService;

        public StoreController(IGenericCRUD<StoreAddRequest, StoreUpdateRequest> storeService)
        {
            _storeService = storeService;
        }

        // GET: api/<StoreController>
        [HttpGet("Full")]
        [SwaggerOperation(Summary = "Lista de Tiendas",
            Description = "Regresa la lista de tiendas activas")]
        public async Task<ActionResult> GetFull([FromQuery] GenricPaginatorN paginator)
        {
            var respon = await _storeService.GetFull(paginator);
            return Ok(respon);
        }

        // GET api/<StoreController>/5
        [HttpGet("OneById/{id}")]
        [SwaggerOperation(Summary = "Tienda por Id",
            Description = "Regresa una tienda de acuerdo a la Id")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var respon =  await _storeService.GetById(id);
            return Ok(respon);
        }

        // POST api/<StoreController>
        [HttpPost]
        [SwaggerOperation(Summary = "Agregar Tienda",
            Description = "Agrega una tienda con estado activo")]
        public async Task<ActionResult> Post([FromBody] StoreAddRequest model)
        {
            var respon = await _storeService.Add(model);
            return Ok(respon);
        }

        // PUT api/<StoreController>/5
        [HttpPut]
        [SwaggerOperation(Summary = "Actualizar Tienda",
            Description = "Actualiza la tienda siempre y cuando los datos son correctos ")]
        public async Task<ActionResult> Put([FromBody] StoreUpdateRequest model)
        {
            var respon = await _storeService.Update(model);
            return Ok(respon);
        }

        // DELETE api/<StoreController>/5
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Desactivar Tienda",
            Description = "Desactiva el articulo de las peticiones")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var respon = await _storeService.DeleteLogic(id);
            return Ok(respon);
        }
    }
}
