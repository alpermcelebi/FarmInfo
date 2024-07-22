namespace FarmInfo.Models
{
    public class Farmer
    {
        public static object? Claims { get; internal set; }
        public int Id { get; set; }
        public string? Username { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public List<Cow>? Cows { get; set; }

    }
}
