using FarmInfo.Models;

namespace FarmInfo.Repositories.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Cow> Cows { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<MilkProductionRecord> MilkProductionRecords { get; set; }
    }
}
