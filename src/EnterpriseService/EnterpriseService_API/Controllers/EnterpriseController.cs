using EnterpriseService_Application.DTOs;
using EnterpriseService_Application.Services.Interfaces;
using EnterpriseService_Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseService_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnterpriseController : ControllerBase
    {
        private readonly IEnterpriseService _enterpriseService;
        public EnterpriseController(IEnterpriseService enterpriseService)
        {
            _enterpriseService = enterpriseService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Enterprise>> Get(int id)
        {
            try
            {
                var enterprise = await _enterpriseService.Get(id);
                return Ok(enterprise);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EnterpriseDTO enterpriseDTO)
        {
            try
            {
                await _enterpriseService.Add(enterpriseDTO);
                return Ok(); 
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPatch]
        public async Task<ActionResult> Patch([FromBody] EnterpriseDTO enterpriseDTO)
        {
            try
            {
                await _enterpriseService.Update(enterpriseDTO);
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _enterpriseService.Delete(id);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
