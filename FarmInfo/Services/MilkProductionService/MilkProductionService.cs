using FarmInfo.Dtos.ProductionDtos;
using FarmInfo.Models;

namespace FarmInfo.Services.MilkProductionService
{
    public class MilkProductionService : IMilkProductionService
    {
        public Task<Response<List<GetProductionRecordDto>>> AddMilkProductionRecord(AddProductionRecordDto newRecord, int cowId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<GetProductionRecordDto>>> DeleteMilkProductionRecord(int cowId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<GetProductionRecordDto>>> GetMilkProductions(int cowId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<GetProductionRecordDto>> UpdateMilkProductionRecord(UpdateProductionRecordDto updatedRecord, int cowId)
        {
            throw new NotImplementedException();
        }
    }
}
