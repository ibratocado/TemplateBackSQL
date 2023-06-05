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
    [SwaggerTag("CRUD paar manejo de Usuarios")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly IGenericCRUDService<AddUserRequest, UpdateUserRequest, GenericPaginatorRequest<string>, Guid> _userService;

        public UsersController(IGenericCRUDService<AddUserRequest, UpdateUserRequest, GenericPaginatorRequest<string>, Guid> userService)
        {
            _userService = userService;
        }


        // GET: api/<UsersController>
        [HttpGet("GetPage")]
        [SwaggerOperation(Summary = "Get Con Paginacion", 
            Description = "Regresa una lista de usuarios, con pagina y tamaño definido, con datos para paginacion, pagina inicial 0")]
        public async Task<ActionResult> GetPage([FromQuery] GenericPaginatorRequest<string> paginator)
        {
            var respon = await _userService.GetByPage(paginator);
            return Ok(respon);
        }

        // GET api/<UsersController>/5
        [HttpGet("GetById/{id}")]
        [SwaggerOperation(Summary = "Get Por Id",
            Description = "Regresa Un usuario dependiendo la id")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var respon = await _userService.GetById(id);
            return Ok(respon);
        }

        // POST api/<UsersController>
        [HttpPost]
        [SwaggerOperation(Summary = "Add de usuario",
            Description = "Resive un usuario para agregarlo a la base")]
        public async Task<ActionResult> Post([FromBody] AddUserRequest value)
        {
            var respon = await _userService.Add(value);
            return Ok(respon);
        }

        // PUT api/<UsersController>/5
        [HttpPut]
        [SwaggerOperation(Summary = "Update de usuario",
            Description = "Resive un usuario para modificarlo en la base")]
        public async Task<ActionResult> Put([FromBody] UpdateUserRequest value)
        {
            var respon = await _userService.Update(value);
            return Ok(respon);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("DeleteLogic/{id}")]
        [SwaggerOperation(Summary = "Delete de usuario",
            Description = "Resive un Id y desactiva al usuario")]
        public async Task<ActionResult> DeleteLogic(Guid id)
        {
            var respon = await _userService.DeleteLogic(id);
            return Ok(respon);
        }
    }
}
