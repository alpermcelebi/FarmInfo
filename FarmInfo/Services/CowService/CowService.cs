using AutoMapper;
using FarmInfo.Dtos.CowDtos;
using FarmInfo.Models;
using FarmInfo.Repositories.CowRepo;

namespace FarmInfo.Services.CowService
{
    public class CowService : ICowService
    {
        private readonly ICowRepository _repository;
        private readonly IMapper _mapper;

        public CowService(ICowRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Response<List<GetCowDto>>> AddCow(AddCowDto newCow)
        {
            var serviceResponse = new Response<List<GetCowDto>>();
            var cow = _mapper.Map<Cow>(newCow);

            await _repository.AddCow(cow);
            var cows = await _repository.GetAllCows();
            serviceResponse.Value = cows.Select(c => _mapper.Map<GetCowDto>(c)).ToList();
            return serviceResponse;

        }

        public async Task<Response<List<GetCowDto>>> DeleteCow(int id)
        {
            var serviceResponse = new Response<List<GetCowDto>>();
            try
            {
                var cow = await _repository.GetCowById(id);
                if (cow == null)
                    throw new Exception($"Character with ID '{id}' not found.");

                await _repository.DeleteCow(cow);
                var cows = await _repository.GetAllCows();
                serviceResponse.Value = cows.Select(c => _mapper.Map<GetCowDto>(c)).ToList();


            }
            catch (Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<Response<List<GetCowDto>>> GetAllCows()
        {
            var serviceResponse = new Response<List<GetCowDto>>();
            var cows = await _repository.GetAllCows();
            serviceResponse.Value = cows.Select(c => _mapper.Map<GetCowDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<Response<GetCowDto>> GetCowById(int id)
        {
            var serviceResponse = new Response<GetCowDto>();    
            var cow = await _repository.GetCowById(id);
            serviceResponse.Value = _mapper.Map<GetCowDto>(cow);
            return serviceResponse;
        }

        public async Task<Response<GetCowDto>> UpdateCow(UpdateCowDto updatedCow)
        {
            var serviceResponse = new Response<GetCowDto>();

            try
            {
                var cow = await _repository.GetCowById(updatedCow.Id);
                if (cow == null) throw new Exception($"Character with Id '{updatedCow.Id}' not found.");

                cow.Name = updatedCow.Name;
                cow.Breed = updatedCow.Breed;
                cow.Age = updatedCow.Age;

                await _repository.UpdateCow(cow);

                serviceResponse.Value = _mapper.Map<GetCowDto>(cow);
                

            }
            catch
            {
                serviceResponse.Success = false;
                serviceResponse.Message = string.Empty;
            }
            return serviceResponse;
        }
    }
}
