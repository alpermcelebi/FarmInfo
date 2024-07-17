using System.Text.Json.Serialization;

namespace FarmInfo.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]

    public enum HealthCondition
    {
        Healthy,
        Mastitis,
        Lameness,
        RespiratoryInfection,
        DigestiveIssue,
        SkinDisease,
        EyeInfection,
        ReproductiveIssue,
        Other
    }
}
