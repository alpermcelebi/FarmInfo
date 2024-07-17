namespace FarmInfo.Models
{
    public class MilkProductionRecord
    {
        public int Id { get; set; }
        public int CowId { get; set; }
        public float Quantity { get; set; }
        public DateTime Date { get; set; } 

        public Cow? Cow { get; set; }
    }
}
