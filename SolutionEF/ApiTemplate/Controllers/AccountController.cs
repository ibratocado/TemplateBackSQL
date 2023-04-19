using ApiTemplate.DTO.Request;
using ApiTemplate.DTO.Respon;
using ApiTemplate.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTemplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Verificacion de Credenciales")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountVerifyService _accountVerifyService;

        public AccountController(IAccountVerifyService accountVerifyService)
        {
            _accountVerifyService = accountVerifyService;
        }

        // POST api/<AccountController>
        [HttpPost]
        [SwaggerOperation(Summary = "Loggin",
            Description = "Verifica que las credenciales mandadas sean existentes ademas regresa un token")]
        public ActionResult<GenericRespon> Post([FromBody] AccountRequest value)
        {
            return StatusCode(StatusCodes.Status200OK, new { respon = new GenericRespon()});
        }
    }
}
