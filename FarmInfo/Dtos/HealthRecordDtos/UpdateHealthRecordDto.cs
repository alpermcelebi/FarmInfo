﻿using FarmInfo.Models.Enums;

namespace FarmInfo.Dtos.HealthRecordDtos
{
    public class UpdateHealthRecordDto
    {
        public int Id { get; set; }

        public HealthCondition Condition { get; set; } = HealthCondition.Healthy;
        public Treatment CurrentTreatment { get; set; } = Treatment.Nothing;
        public DateTime Date { get; set; }

    }
}
