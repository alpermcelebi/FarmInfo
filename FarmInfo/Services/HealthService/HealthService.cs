using AutoMapper;
using FarmInfo.Dtos.HealthRecordDtos;
using FarmInfo.Models;
using FarmInfo.Repositories.CowRepo;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FarmInfo.Services.HealthService
{
    public class HealthService : IHealthService
    {
        private readonly ICowRepository _repository;
        private readonly IMapper _mapper;

        public HealthService(ICowRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetHealthRecordDto>>> AddHealthRecord(AddHealthRecordDto newHealthRecord, int cowId)
        {
            var serviceResponse = new ServiceResponse<List<GetHealthRecordDto>>();
            try
            {
                var cow = await _repository.GetCowById(cowId) ?? throw new Exception($"Cow with ID '{cowId}' not found.");

                var healthRecord = _mapper.Map<HealthRecord>(newHealthRecord);
                await _repository.AddHealthRecord(healthRecord, cowId);

                var records = await _repository.GetHealthRecords(cowId);
                serviceResponse.Value = records.Select(r => _mapper.Map<GetHealthRecordDto>(r)).ToList();

                serviceResponse.Message = "Health record added successfully";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Failed to add health record: {ex.Message}";
            }

            return serviceResponse;
        }


        public async Task<ServiceResponse<List<GetHealthRecordDto>>> DeleteHealthRecord(GetHealthRecordDto healthRecord, int cowId)
        {
            var serviceResponse = new ServiceResponse<List<GetHealthRecordDto>>();
            try
            {
                var cow = await _repository.GetCowById(cowId) ?? throw new Exception($"Cow with ID '{cowId}' not found.");

                var record = _mapper.Map<HealthRecord>(healthRecord);
                if (await _repository.DeleteHealthRecord(record, cowId) == false) throw new Exception($"Record with ID '{healthRecord.Id}' not found.");

                var healthRecords = await _repository.GetHealthRecords(cowId);
                serviceResponse.Value = healthRecords.Select(r => _mapper.Map<GetHealthRecordDto>(r)).ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetHealthRecordDto>>> GetHealthRecords(int cowId)
        {
            var serviceResponse = new ServiceResponse<List<GetHealthRecordDto>>();
            if( await _repository.GetCowById(cowId) == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Invalid cow ID.";
                return serviceResponse;
            }
            var healthRecords = await _repository.GetHealthRecords(cowId);
            serviceResponse.Value = healthRecords.Select(c => _mapper.Map<GetHealthRecordDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetHealthRecordDto>>> UpdateHealthRecord(UpdateHealthRecordDto updatedHealthRecord, int cowId)
        {
            var serviceResponse = new ServiceResponse<List<GetHealthRecordDto>>();
            try
            {
                var cow = await _repository.GetCowById(cowId) ?? throw new Exception($"Cow with ID '{cowId}' not found.");

                var updatedRecord = _mapper.Map<HealthRecord>(updatedHealthRecord);
                if (await _repository.UpdateHealthRecord(updatedRecord, cowId) == false) throw new Exception($"Health record not found with '{updatedRecord.Id}'.");
                
                var healthRecord = cow.HealthRecords!.FirstOrDefault(hr => hr.Id == updatedHealthRecord.Id);
                healthRecord!.Condition = updatedRecord.Condition;
                healthRecord!.CurrentTreatment = updatedRecord.CurrentTreatment;
                healthRecord!.Date = updatedRecord.Date;
                var healthRecords = await _repository.GetHealthRecords(cowId);
                serviceResponse.Value = healthRecords.Select(r => _mapper.Map<GetHealthRecordDto>(r)).ToList();
                serviceResponse.Message = "Record has been updated successfully.";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
