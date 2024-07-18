using Azure;
using FarmInfo.Dtos.ProductionDtos;
using FarmInfo.Services.MilkProductionService;
using Microsoft.AspNetCore.Mvc;

namespace FarmInfo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MilkProductionRecordsController : ControllerBase
    {
        private readonly IMilkProductionService _milkProductionService;

        public MilkProductionRecordsController(IMilkProductionService milkProductionService)
        {
            _milkProductionService = milkProductionService;
        }

        // Get all milk production records for a specific cow
        [HttpGet("{cowId}")]
        public async Task<ActionResult<Response<List<GetProductionRecordDto>>>> Get(int cowId)
        {
            var response = await _milkProductionService.GetMilkProductions(cowId);
            if (response.Value == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // Add a new milk production record to a specific cow
        [HttpPost("{cowId}")]
        public async Task<ActionResult<Response<List<GetProductionRecordDto>>>> Add(int cowId, AddProductionRecordDto newRecord)
        {
            var response = await _milkProductionService.AddMilkProductionRecord(newRecord, cowId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        // Update a milk production record of a specific cow
        [HttpPut("{cowId}")]
        public async Task<ActionResult<Response<List<GetProductionRecordDto>>>> Update(int cowId, UpdateProductionRecordDto updatedRecord)
        {
            var response = await _milkProductionService.UpdateMilkProductionRecord(updatedRecord, cowId);
            if (response.Value == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // Delete a milk production record of a specific cow
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<List<GetProductionRecordDto>>>> Delete(GetProductionRecordDto productionRecord, int id)
        {
            var response = await _milkProductionService.DeleteMilkProductionRecord(productionRecord, id);
            if (response.Value == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
