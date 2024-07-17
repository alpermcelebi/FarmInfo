using Azure;
using FarmInfo.Dtos.HealthRecordDtos;
using FarmInfo.Services.HealthService;
using Microsoft.AspNetCore.Mvc;

namespace FarmInfo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly IHealthService _healthService;

        public HealthController(IHealthService healthService)
        {
            _healthService = healthService;
        }

        // Get all health records for a specific cow
        [HttpGet("{cowId}")]
        public async Task<ActionResult<Response<List<GetHealthRecordDto>>>> Get(int cowId)
        {
            var response = await _healthService.GetHealthRecords(cowId);
            if (response.Value == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // Add a new health record to a specific cow
        [HttpPost("{cowId}")]
        public async Task<ActionResult<Response<List<GetHealthRecordDto>>>> Add(int cowId, AddHealthRecordDto newHealth)
        {
            var response = await _healthService.AddHealthRecord(newHealth, cowId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        // Update a health record of a specific cow
        [HttpPut("{cowId}")]
        public async Task<ActionResult<Response<List<GetHealthRecordDto>>>> Update(int cowId, UpdateHealthRecordDto updatedHealth)
        {
            var response = await _healthService.UpdateHealthRecord(updatedHealth, cowId);
            if (response.Value == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // Delete a health record of a specific cow
        [HttpDelete("{cowId}/{recordId}")]
        public async Task<ActionResult<Response<List<GetHealthRecordDto>>>> Delete(int cowId, int recordId)
        {
            var response = await _healthService.DeleteHealthRecord(new GetHealthRecordDto { Id = recordId }, cowId);
            if (response.Value == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
