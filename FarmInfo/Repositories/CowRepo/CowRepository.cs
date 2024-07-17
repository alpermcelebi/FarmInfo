using FarmInfo.Models;
using FarmInfo.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FarmInfo.Repositories.CowRepo
{
    public class CowRepository : ICowRepository
    {
        private readonly DataContext _context;

        public CowRepository(DataContext context)
        {
            _context = context; // Dependency Injection
        }
        public async Task AddCow(Cow cow)
        {
            await _context.Cows.AddAsync(cow);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteCow(Cow cow)
        {
            _context.Remove(cow);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Cow>> GetAllCows()
        {
            return await _context.Cows.ToListAsync();
        }

        public async Task<Cow> GetCowById(int id)
        {
            var cow = await _context.Cows.FirstOrDefaultAsync(c => c.Id == id);
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

        public async Task UpdateHealthRecord(HealthRecord updatedHealthRecord, int cowId)
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
                }
            }
            
        }

        public async Task<List<HealthRecord>> DeleteHealthRecord(HealthRecord healthRecord, int cowId)
        {
            var cow = await _context.Cows.Include(c => c.HealthRecords).FirstOrDefaultAsync(c => c.Id == cowId);
            if (cow != null) {
                if (healthRecord != null)
                {
                    cow.HealthRecords!.Remove(healthRecord);
                    await _context.SaveChangesAsync();
                }
            }
            return cow.HealthRecords;
        }
    }
}
