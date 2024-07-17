namespace FarmInfo.Models
{
    public class Cow
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Jersey";
        public string? Breed { get; set; }
        public int Age { get; set; }
        public List<HealthRecord>? HealthRecords { get; set; } = new List<HealthRecord>();
        public List<MilkProductionRecord>? MilkProductionRecords { get; set; } = new List<MilkProductionRecord>();

    }
}
