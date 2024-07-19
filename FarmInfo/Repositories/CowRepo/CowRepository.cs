using FarmInfo.Models;
using FarmInfo.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FarmInfo.Repositories.CowRepo
{
    public class CowRepository : ICowRepository
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CowRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context; // Dependency Injection
            _httpContextAccessor = httpContextAccessor;
        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task AddCow(Cow cow)
        {
            cow.Farmer = await _context.Farmers.FirstOrDefaultAsync(f => f.Id == GetUserId());
            await _context.Cows.AddAsync(cow);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteCow(Cow cow)
        {
            _context.Remove(cow);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Cow>> GetAllCows(int id)
        {
            return await _context.Cows.Where(c => c.Farmer!.Id == id).ToListAsync();
        }

        public async Task<List<Cow>> GetAllCows()
        {
            return await _context.Cows.ToListAsync();
        }

        public async Task<Cow> GetCowById(int id)
        {
            var cow = await _context.Cows.FirstOrDefaultAsync(c => c.Id == id && c.Farmer!.Id == GetUserId());
            return cow!;
        }

        public async Task UpdateCow(Cow cow)
        {
            _context.Cows.Update(cow);
            await _context.SaveChangesAsync();
        }
        // batch operations below
        public async Task AddCows(List<Cow> cows)
        {
            await _context.Cows.AddRangeAsync(cows);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCows(List<Cow> cows)
        {
            _context.Cows.UpdateRange(cows);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCows(List<Cow> cows)
        {
            _context.Cows.RemoveRange(cows);
            await _context.SaveChangesAsync();
        }
        // health record operations below
        public async Task<List<HealthRecord>> GetHealthRecords(int cowId)
        {
            var cow = await _context.Cows.Include(c => c.HealthRecords).FirstOrDefaultAsync(c => c.Id == cowId);
            var healthRecords = cow!.HealthRecords;
            return cow?.HealthRecords ?? new List<HealthRecord>();

        }

        public async Task AddHealthRecord(HealthRecord newHealthRecord, int cowId)
        {
            var cow = await _context.Cows.Include(c => c.HealthRecords).FirstOrDefaultAsync(c => c.Id == cowId);
            if (cow != null)
            {
                cow.HealthRecords!.Add(newHealthRecord);
                await _context.HealthRecords.AddAsync(newHealthRecord);
                await _context.SaveChangesAsync();
            }
            
            
        }

        public async Task<bool> UpdateHealthRecord(HealthRecord updatedHealthRecord, int cowId)
        {
            var cow = await _context.Cows.Include(c => c.HealthRecords).FirstOrDefaultAsync(c => c.Id == cowId);
            if (cow != null)
            {
                var healthRecord = cow.HealthRecords!.FirstOrDefault(hr => hr.Id == updatedHealthRecord.Id);
                if (healthRecord != null)
                {
                    healthRecord.Condition = updatedHealthRecord.Condition;
                    healthRecord.CurrentTreatment = updatedHealthRecord.CurrentTreatment;
                    healthRecord.Date = updatedHealthRecord.Date;
                    _context.HealthRecords.Update(healthRecord);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            return false;
            
        }

        public async Task<bool> DeleteHealthRecord(HealthRecord healthRecord, int cowId)
        {
            var cow = await _context.Cows.Include(c => c.HealthRecords).FirstOrDefaultAsync(c => c.Id == cowId);
            if (cow != null)
            {
                var deletedRecord = cow.HealthRecords!.FirstOrDefault(hr => hr.Id == healthRecord.Id);
                if (deletedRecord != null)
                {
                    cow.HealthRecords!.Remove(deletedRecord);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            return false;
        }

        
        public async Task<List<MilkProductionRecord>> GetProductionRecords(int cowId)
        {
            var cow = await _context.Cows.Include(c => c.MilkProductionRecords).FirstOrDefaultAsync(c => c.Id == cowId);
            var healthRecords = cow!.HealthRecords;
            return cow?.MilkProductionRecords ?? new List<MilkProductionRecord>();
        }

        public async Task AddProductionRecord(MilkProductionRecord productionRecord, int cowId)
        {
            var cow = await _context.Cows.Include(c => c.MilkProductionRecords).FirstOrDefaultAsync(c => c.Id == cowId);
            if (cow != null)
            {
                cow.MilkProductionRecords!.Add(productionRecord);
                await _context.MilkProductionRecords.AddAsync(productionRecord);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> UpdateProductionRecord(MilkProductionRecord updatedProductionRecord, int cowId)
        {
            var cow = await _context.Cows.Include(c => c.MilkProductionRecords).FirstOrDefaultAsync(c => c.Id == cowId);
            if (cow != null)
            {
                var productionRecord = cow.MilkProductionRecords!.FirstOrDefault(hr => hr.Id == updatedProductionRecord.Id);
                if (productionRecord != null)
                {
                    productionRecord.Quantity = updatedProductionRecord.Quantity;
                    productionRecord.Date = updatedProductionRecord.Date;

                    _context.MilkProductionRecords.Update(productionRecord);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            return false;
        }

        public async Task<bool> DeleteProductionRecord(MilkProductionRecord productionRecord, int cowId)
        {
            var cow = await _context.Cows.Include(c => c.MilkProductionRecords).FirstOrDefaultAsync(c => c.Id == cowId);
            if (cow != null)
            {
                var deletedRecord = cow.MilkProductionRecords!.FirstOrDefault(hr => hr.Id == productionRecord.Id);
                if (deletedRecord != null)
                {
                    cow.MilkProductionRecords!.Remove(deletedRecord);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
