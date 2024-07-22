
using FarmInfo.Dtos.CowDtos;
using FarmInfo.Models;
using FarmInfo.Services.CowService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FarmInfo.Controllers
{
    [Authorize]   
    [ApiController]
    [Route("api/[controller]")]
    public class CowsController : ControllerBase
    {
        private readonly ICowService _cowService;

        public CowsController(ICowService cowService)
        {
            _cowService = cowService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCowDto>>>> Get()
        {
            return Ok(await _cowService.GetAllCows());
        }
        [HttpGet("GetSingleCow{id}")]
        public async Task<ActionResult<ServiceResponse<GetCowDto>>> GetSingle(int id)
        {
            var returnResponse = await _cowService.GetCowById(id);
            if (returnResponse.Success == false) return BadRequest(returnResponse);
            return Ok(returnResponse);
        }
        [HttpPost("CreateCow")]

        public async Task<ActionResult<ServiceResponse<List<GetCowDto>>>> Add(AddCowDto newCow)
        {
            return Ok(await _cowService.AddCow(newCow));
        }
        [HttpPut("UpdateCow")]
        public async Task<ActionResult<ServiceResponse<List<GetCowDto>>>> Update(UpdateCowDto updatedCow)
        {
            var response = await _cowService.UpdateCow(updatedCow);
            if (response.Value == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("DeleteCow")]

        public async Task<ActionResult<ServiceResponse<GetCowDto>>> Delete(int id)
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
