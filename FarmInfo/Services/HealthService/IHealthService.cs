using FarmInfo.Dtos.HealthRecordDtos;
using FarmInfo.Models;

namespace FarmInfo.Services.HealthService
{
    public interface IHealthService
    {
        Task<Response<List<GetHealthRecordDto>>> GetHealthRecords(int cowId);
        Task<Response<List<GetHealthRecordDto>>> AddHealthRecord(AddHealthRecordDto newHealthRecord, int cowId);
        Task<Response<List<GetHealthRecordDto>>> UpdateHealthRecord(UpdateHealthRecordDto updatedHealthRecord, int cowId);
        Task<Response<List<GetHealthRecordDto>>> DeleteHealthRecord(GetHealthRecordDto healthRecord, int cowId);
    }
}
