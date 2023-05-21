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
    [SwaggerTag("CRUD Para Manejo de Clientes")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomersController : ControllerBase
    {
        private readonly IGenericCRUD<CustomerAddRequest, CustomerUpdateRequest> _customerService;

        public CustomersController(IGenericCRUD<CustomerAddRequest, CustomerUpdateRequest> customerService)
        {
            _customerService = customerService;
        }

        // GET: api/<StoreController>
        [HttpGet("Full")]
        [SwaggerOperation(Summary = "Lista de Clientes",
            Description = "Regresa la lista de Clientes activas")]
        public async Task<ActionResult> GetFull([FromQuery] GenricPaginatorN paginator)
        {
            var respon = await _customerService.GetFull(paginator);
            return Ok(respon);
        }

        // GET api/<StoreController>/5
        [HttpGet("OneById/{id}")]
        [SwaggerOperation(Summary = "Cliente por Id",
            Description = "Regresa una Cliente de acuerdo a la Id")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var respon = await _customerService.GetById(id);
            return Ok(respon);
        }

        // POST api/<StoreController>
        [HttpPost]
        [SwaggerOperation(Summary = "Agregar Cliente",
            Description = "Agrega una Cliente con estado activo")]
        public async Task<ActionResult> Post([FromBody] CustomerAddRequest model)
        {
            var respon = await _customerService.Add(model);
            return Ok(respon);
        }

        // PUT api/<StoreController>/5
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualizar Cliente",
            Description = "Actualiza la Cliente siempre y cuando los datos son correctos ")]
        public async Task<ActionResult> Put([FromBody] CustomerUpdateRequest model)
        {
            var respon = await _customerService.Update(model);
            return Ok(respon);
        }

        // DELETE api/<StoreController>/5
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Desactivar Cliente",
            Description = "Desactiva el articulo de las peticiones")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var respon = await _customerService.DeleteLogic(id);
            return Ok(respon);
        }
    }
}
