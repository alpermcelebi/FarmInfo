
using FarmInfo.Dtos.CowDtos;
using FarmInfo.Models;
using FarmInfo.Services.CowService;
using Microsoft.AspNetCore.Mvc;

namespace FarmInfo.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class CowController : ControllerBase
    {
        private readonly ICowService _cowService;

        public CowController(ICowService cowService)
        {
            _cowService = cowService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCowDto>>>> Get()
        {
            return Ok(await _cowService.GetAllCows());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCowDto>>> GetSingle(int id)
        {
            return Ok(await _cowService.GetCowById(id));
        }
        [HttpPost]

        public async Task<ActionResult<ServiceResponse<List<GetCowDto>>>> AddCharacter(AddCowDto newCow)
        {
            return Ok(await _cowService.AddCow(newCow));
        }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetCowDto>>>> UpdateCharacter(UpdateCowDto updatedCow)
        {
            var response = await _cowService.UpdateCow(updatedCow);
            if (response.Value == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete]

        public async Task<ActionResult<ServiceResponse<GetCowDto>>> DeleteCharacter(int id)
        {
            var response = await _cowService.DeleteCow(id);
            if (response.Value == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
