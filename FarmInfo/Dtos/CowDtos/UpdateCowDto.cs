﻿namespace FarmInfo.Dtos.CowDtos
{
    public class UpdateCowDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Sarikiz";
        public string? Breed { get; set; } = "None";
        public int Age { get; set; }
    }
}
