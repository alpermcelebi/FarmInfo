using FarmInfo.Dtos.HealthRecordDtos;
using FarmInfo.Dtos.ProductionDtos;
using FarmInfo.Models;

namespace FarmInfo.Services.MilkProductionService
{
    public interface IMilkProductionService
    {
        Task<ServiceResponse<List<GetProductionRecordDto>>> GetMilkProductions(int cowId);
        Task<ServiceResponse<List<GetProductionRecordDto>>> AddMilkProductionRecord(AddProductionRecordDto newRecord, int cowId);
        Task<ServiceResponse<GetProductionRecordDto>> UpdateMilkProductionRecord(UpdateProductionRecordDto updatedRecord, int cowId);
        Task<ServiceResponse<List<GetProductionRecordDto>>> DeleteMilkProductionRecord(GetProductionRecordDto productionRecord,int cowId);
    }
}
