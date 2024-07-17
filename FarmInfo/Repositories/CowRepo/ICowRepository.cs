using FarmInfo.Dtos.HealthRecordDtos;
using FarmInfo.Models;

namespace FarmInfo.Repositories.CowRepo
{
    public interface ICowRepository
    {
        Task AddCow(Cow cow);
        Task DeleteCow(Cow cow);
        Task<Cow> GetCowById(int id);
        Task<List<Cow>> GetAllCows();
        Task UpdateCow(Cow cow);

        Task<List<HealthRecord>> GetHealthRecords(int cowId);
        Task<HealthRecord> AddHealthRecord(HealthRecord newHealthRecord, int cowId);
        Task<HealthRecord> UpdateHealthRecord(HealthRecord updatedHealthRecord, int cowId);
        Task<List<HealthRecord>> DeleteHealthRecord(HealthRecord healthRecord, int cowId);
        
    }
}
