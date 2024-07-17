using FarmInfo.Dtos.ProductionDtos;
using FarmInfo.Models;

namespace FarmInfo.Services.MilkProductionService
{
    public interface IMilkProductionService
    {
        Task<Response<List<GetProductionRecordDto>>> GetMilkProductions(int cowId);
        Task<Response<List<GetProductionRecordDto>>> AddMilkProductionRecord(AddProductionRecordDto newRecord, int cowId);
        Task<Response<GetProductionRecordDto>> UpdateMilkProductionRecord(UpdateProductionRecordDto updatedRecord, int cowId);
        Task<Response<List<GetProductionRecordDto>>> DeleteMilkProductionRecord(int cowId);
    }
}
