using FarmInfo.Dtos.HealthRecordDtos;
using FarmInfo.Models;

namespace FarmInfo.Services.HealthService
{
    public interface IHealthService
    {
        Task<ServiceResponse<List<GetHealthRecordDto>>> GetHealthRecords(int cowId);
        Task<ServiceResponse<List<GetHealthRecordDto>>> AddHealthRecord(AddHealthRecordDto newHealthRecord, int cowId);
        Task<ServiceResponse<List<GetHealthRecordDto>>> UpdateHealthRecord(UpdateHealthRecordDto updatedHealthRecord, int cowId);
        Task<ServiceResponse<List<GetHealthRecordDto>>> DeleteHealthRecord(GetHealthRecordDto healthRecord, int cowId);
    }
}
