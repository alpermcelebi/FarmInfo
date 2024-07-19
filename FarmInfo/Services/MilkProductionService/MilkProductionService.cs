using AutoMapper;
using FarmInfo.Dtos.HealthRecordDtos;
using FarmInfo.Dtos.ProductionDtos;
using FarmInfo.Models;
using FarmInfo.Repositories.CowRepo;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FarmInfo.Services.MilkProductionService
{
    public class MilkProductionService : IMilkProductionService
    {
        private readonly ICowRepository _repository;
        private readonly IMapper _mapper;

        public MilkProductionService(ICowRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetProductionRecordDto>>> AddMilkProductionRecord(AddProductionRecordDto newRecord, int cowId)
        {
            var sServiceResponse = new ServiceResponse<List<GetProductionRecordDto>>();
            try
            {
                var cow = await _repository.GetCowById(cowId) ?? throw new Exception($"Cow with ID '{cowId}' not found.");

                var productionRecord = _mapper.Map<MilkProductionRecord>(newRecord);
                await _repository.AddProductionRecord(productionRecord, cowId);

                var records = await _repository.GetProductionRecords(cowId);
                sServiceResponse.Value = records.Select(r => _mapper.Map<GetProductionRecordDto>(r)).ToList();

                sServiceResponse.Message = "Milk production record added successfully";
            }
            catch (Exception ex)
            {
                sServiceResponse.Success = false;
                sServiceResponse.Message = $"Failed to add milk production record: {ex.Message}";
            }

            return sServiceResponse;
        }

        public async Task<ServiceResponse<List<GetProductionRecordDto>>> DeleteMilkProductionRecord(GetProductionRecordDto productionRecord, int cowId)
        {
            var sServiceResponse = new ServiceResponse<List<GetProductionRecordDto>>();
            try
            {
                var cow = await _repository.GetCowById(cowId) ?? throw new Exception($"Cow with ID '{cowId}' not found.");

                var record = _mapper.Map<MilkProductionRecord>(productionRecord);
                if (await _repository.DeleteProductionRecord(record, cowId) == false) throw new Exception($"Record with ID '{productionRecord.Id}' not found.");

                var productionRecords = await _repository.GetProductionRecords(cowId);
                sServiceResponse.Value = productionRecords.Select(r => _mapper.Map<GetProductionRecordDto>(r)).ToList();

            }
            catch (Exception ex)
            {
                sServiceResponse.Success = false;
                sServiceResponse.Message = ex.Message;
            }
            return sServiceResponse;
        }

        public async Task<ServiceResponse<List<GetProductionRecordDto>>> GetMilkProductions(int cowId)
        {
            var serviceResponse = new ServiceResponse<List<GetProductionRecordDto>>();
            if (await _repository.GetCowById(cowId) == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Invalid cow ID.";
                return serviceResponse;
            }
            var productionRecords = await _repository.GetProductionRecords(cowId);
            serviceResponse.Value = productionRecords.Select(c => _mapper.Map<GetProductionRecordDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductionRecordDto>> UpdateMilkProductionRecord(UpdateProductionRecordDto updatedRecord, int cowId)
        {
            var sServiceResponse = new ServiceResponse<GetProductionRecordDto>();
            try
            {
                var cow = await _repository.GetCowById(cowId) ?? throw new Exception($"Cow with ID '{cowId}' not found.");

                var productionRecord = _mapper.Map<MilkProductionRecord>(updatedRecord);
                if (await _repository.UpdateProductionRecord(productionRecord, cowId) == false) throw new Exception($"Production with ID '{productionRecord.Id}' not found.");

                var updatedRecordDto = _mapper.Map<GetProductionRecordDto>(productionRecord);

                sServiceResponse.Value = updatedRecordDto;
                sServiceResponse.Message = "Milk production record updated successfully";
            }
            catch (Exception ex)
            {
                sServiceResponse.Success = false;
                sServiceResponse.Message = $"Failed to update milk production record: {ex.Message}";
            }

            return sServiceResponse;
        }
    }
}
