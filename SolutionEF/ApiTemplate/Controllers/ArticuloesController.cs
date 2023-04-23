using Microsoft.AspNetCore.Mvc;
using ApiTemplate.Services.Interfaces;
using ApiTemplate.DTO.Respon;
using ApiTemplate.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ApiTemplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ArticuloesController : ControllerBase
    {
        private readonly IArticulosService _articulosService;

        public ArticuloesController(IArticulosService articulosService)
        {
            _articulosService = articulosService;
        }
        
        [HttpGet("All")]
        public async Task<ActionResult<GenericRespon>> GetArticulos([FromQuery] int page, [FromQuery] int records)
        {
            var respon = await _articulosService.GetAll(page,records);
            return StatusCode(respon.State, new { respon = respon });
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Articulo>> GetArticulo(Guid id)
        {
            var respon = await _articulosService.GetOne(id);
            return StatusCode(respon.State, new {respon = respon});
        }

        
        [HttpPut("Update")]
        public async Task<IActionResult> PutArticulo(ArticlesPutRequest articulo)
        {
            var respon = await _articulosService.Update(articulo);
            return StatusCode(respon.State, new {respon = respon});
        }

        
        [HttpPost("Insert")]
        public async Task<ActionResult<Articulo>> PostArticulo(ArticlesPostRequest articulo)
        {
            var respon = await _articulosService.Insert(articulo);
            return StatusCode(respon.State, new { respon = respon });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteArticulo(Guid id)
        {
            var respon = await _articulosService.Delete(id);
            return StatusCode(respon.State, new { respon = respon });
        }
    }
}
