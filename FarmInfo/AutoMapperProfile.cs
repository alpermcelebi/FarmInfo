using AutoMapper;
using FarmInfo.Dtos.CowDtos;
using FarmInfo.Dtos.HealthRecordDtos;
using FarmInfo.Dtos.ProductionDtos;
using FarmInfo.Models;
namespace FarmInfo
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Cow, GetCowDto>();
            CreateMap<AddCowDto, Cow>();
            CreateMap<UpdateCowDto, Cow>();

            CreateMap<AddHealthRecordDto, HealthRecord>();
            CreateMap<HealthRecord, GetHealthRecordDto>();
            CreateMap<UpdateHealthRecordDto, HealthRecord>();

            CreateMap<AddProductionRecordDto, MilkProductionRecord>();
            CreateMap<MilkProductionRecord, GetProductionRecordDto>();
            CreateMap<UpdateProductionRecordDto, MilkProductionRecord>();
        }
    }
}
