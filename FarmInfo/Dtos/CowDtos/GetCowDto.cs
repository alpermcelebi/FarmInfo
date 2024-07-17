namespace FarmInfo.Dtos.CowDtos
{
    public class GetCowDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Sarikiz";
        public string Breed { get; set; } = "Jersey";
        public int Age { get; set; }
    }
}
