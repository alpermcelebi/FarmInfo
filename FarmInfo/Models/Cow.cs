namespace FarmInfo.Models
{
    public class Cow
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Sarikiz";
        public string? Breed { get; set; } = "None";
        public int Age { get; set; }
        public List<HealthRecord>? HealthRecords { get; set; }
        public List<MilkProductionRecord>? MilkProductionRecords { get; set; }

    }
}
