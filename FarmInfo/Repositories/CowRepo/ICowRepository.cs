﻿using FarmInfo.Dtos.HealthRecordDtos;
using FarmInfo.Models;

namespace FarmInfo.Repositories.CowRepo
{
    public interface ICowRepository
    {
        Task AddCow(Cow cow);
        Task DeleteCow(Cow cow);
        Task<Cow> GetCowById(int id);
        Task<List<Cow>> GetAllCows(int id);
        Task<List<Cow>> GetAllCows();
        Task UpdateCow(Cow cow);
        Task<List<HealthRecord>> GetHealthRecords(int cowId);
        Task AddHealthRecord(HealthRecord newHealthRecord, int cowId);
        Task<bool> UpdateHealthRecord(HealthRecord updatedHealthRecord, int cowId);
        Task<bool> DeleteHealthRecord(HealthRecord healthRecord, int cowId);
        Task<List<MilkProductionRecord>> GetProductionRecords(int cowId);
        Task AddProductionRecord(MilkProductionRecord productionRecord, int cowId);
        Task<bool> UpdateProductionRecord(MilkProductionRecord updatedProductionRecord, int cowId);
        Task<bool> DeleteProductionRecord(MilkProductionRecord productionRecord, int cowId);

    }
}
