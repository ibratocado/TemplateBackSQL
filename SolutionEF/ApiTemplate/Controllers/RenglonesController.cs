using ApiTemplate.DTO.Respon;
using ApiTemplate.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTemplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RenglonesController : ControllerBase
    {
        private readonly IRenglonesService _renglonesService;

        public RenglonesController(IRenglonesService renglonesService)
        {
            _renglonesService = renglonesService;
        }

        // GET: api/<RenglonesController>
        [HttpGet("/Ciclo")]
        public async Task<ActionResult<GenericRespon>> Get(int number)
        {
            var respon = await _renglonesService.Get(number);
            return StatusCode(respon.State, new { respon = respon });
        }

        [HttpGet("/Recursive")]
        public async Task<ActionResult<GenericRespon>> GetRecursive(int number)
        {
            var respon = await _renglonesService.GetRescursive(number);
            return StatusCode(respon.State, new { respon = respon });
        }

    }
}
