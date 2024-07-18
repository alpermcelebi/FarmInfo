using FarmInfo.Models.Enums;

namespace FarmInfo.Models
{
    public class HealthRecord
    {
        public int Id { get; set; }
        public int CowId { get; set; }
        public HealthCondition Condition { get; set; } = HealthCondition.Healthy;
        public Treatment CurrentTreatment { get; set; } = Treatment.Nothing;
        public DateTime Date{ get; set; }

        public Cow Cow { get; set; } // Navigation property (it is optional since CowId already does same thing)
    }
}
