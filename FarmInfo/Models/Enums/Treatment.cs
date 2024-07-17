using System.Text.Json.Serialization;

namespace FarmInfo.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]

    public enum Treatment
    {
        Nothing,
        Antibiotics,
        HoofTrimming,
        Surgery,
        PainRelief,
        Vaccination,
        NutritionalSupplement,
        Isolation,
        Other
    }
}
