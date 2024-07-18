using FarmInfo.Models;

namespace FarmInfo.Repositories.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Cow> Cows { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<MilkProductionRecord> MilkProductionRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cow>()
                .HasMany(c => c.HealthRecords)
                .WithOne(h => h.Cow)
                .HasForeignKey(h => h.CowId);

            modelBuilder.Entity<Cow>()
                .HasMany(c => c.MilkProductionRecords)
                .WithOne(m => m.Cow)
                .HasForeignKey(m => m.CowId);
        }

    }
}
