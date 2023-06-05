using ApiTemplate.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTemplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolsController : ControllerBase
    {

        private readonly IRolsService _rolsService;

        public RolsController(IRolsService rolsService)
        {
            _rolsService = rolsService;
        }


        // GET: api/<RolsController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var respon = await _rolsService.GetFullRols();
            return Ok(respon);
        }

    }
}
