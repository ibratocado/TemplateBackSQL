using ApiTemplate.DTO.Request;
using ApiTemplate.DTO.Respon;
using ApiTemplate.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTemplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IGenericCRUD<StoreAddRequest,StoreUpdateRequest> _storeService;

        public StoreController(IGenericCRUD<StoreAddRequest, StoreUpdateRequest> storeService)
        {
            _storeService = storeService;
        }

        // GET: api/<StoreController>
        [HttpGet("Full")]
        public async Task<GenericRespon> GetFull()
        {
            var respon = await _storeService.GetFull();
            return respon;
        }

        // GET api/<StoreController>/5
        [HttpGet("OneById/{id}")]
        public async Task<GenericRespon> GetById(Guid id)
        {
            var respon =  await _storeService.GetById(id);
            return respon;
        }

        // POST api/<StoreController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] StoreAddRequest model)
        {
            var respon = await _storeService.Add(model);
            return Ok(respon);
        }

        // PUT api/<StoreController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] StoreUpdateRequest model)
        {
            var respon = await _storeService.Update(model);
            return Ok(respon);
        }

        // DELETE api/<StoreController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var respon = await _storeService.DeleteLogic(id);
            return Ok(respon);
        }
    }
}
