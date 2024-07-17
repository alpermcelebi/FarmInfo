using AutoMapper;
using FarmInfo.Dtos.HealthRecordDtos;
using FarmInfo.Models;
using FarmInfo.Repositories.CowRepo;

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
        public async Task<Response<List<GetHealthRecordDto>>> AddHealthRecord(AddHealthRecordDto newHealthRecord, int cowId)
        {
            var serviceResponse = new Response<List<GetHealthRecordDto>>();
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


        public async Task<Response<List<GetHealthRecordDto>>> DeleteHealthRecord(GetHealthRecordDto healthRecord, int cowId)
        {
            var serviceResponse = new Response<List<GetHealthRecordDto>>();
            try
            {
                var cow = await _repository.GetCowById(cowId) ?? throw new Exception($"Cow with ID '{cowId}' not found.");

                var record = _mapper.Map<HealthRecord>(healthRecord);
                await _repository.DeleteHealthRecord(record, cowId);

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

        public async Task<Response<List<GetHealthRecordDto>>> GetHealthRecords(int cowId)
        {
            var serviceResponse = new Response<List<GetHealthRecordDto>>();
            var healthRecords = await _repository.GetHealthRecords(cowId);
            serviceResponse.Value = healthRecords.Select(c => _mapper.Map<GetHealthRecordDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<Response<List<GetHealthRecordDto>>> UpdateHealthRecord(UpdateHealthRecordDto updatedHealthRecord, int cowId)
        {
            var serviceResponse = new Response<List<GetHealthRecordDto>>();
            try
            {
                var cow = await _repository.GetCowById(cowId) ?? throw new Exception($"Cow with ID '{cowId}' not found.");
                var updatedRecord = _mapper.Map<HealthRecord>(updatedHealthRecord);
                await _repository.UpdateHealthRecord(updatedRecord, cowId);

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
    }
}
