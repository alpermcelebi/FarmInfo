using AutoMapper;
using FarmInfo.Dtos.CowDtos;
using FarmInfo.Models;
using FarmInfo.Repositories.CowRepo;
using System.Security.Claims;

namespace FarmInfo.Services.CowService
{
    public class CowService : ICowService
    {
        private readonly ICowRepository _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CowService(ICowRepository repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        public async Task<ServiceResponse<List<GetCowDto>>> AddCow(AddCowDto newCow)
        {
            var serviceResponse = new ServiceResponse<List<GetCowDto>>();
            var cow = _mapper.Map<Cow>(newCow);
            

            await _repository.AddCow(cow);
            var cows = await _repository.GetAllCows();
            serviceResponse.Value = cows
                    .Where(c => c.Farmer != null && c.Farmer.Id == GetUserId())
                    .Select(c => _mapper.Map<GetCowDto>(c))
                    .ToList();
            return serviceResponse;

        }

        public async Task<ServiceResponse<List<GetCowDto>>> DeleteCow(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCowDto>>();
            try
            {
                var cow = await _repository.GetCowById(id);
                if (cow == null)
                    throw new Exception($"Cow with ID '{id}' not found.");

                await _repository.DeleteCow(cow);
                var cows = await _repository.GetAllCows(GetUserId());
                serviceResponse.Value = cows.Select(c => _mapper.Map<GetCowDto>(c)).ToList();


            }
            catch (Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCowDto>>> GetAllCows()
        {
            var serviceResponse = new ServiceResponse<List<GetCowDto>>();
            var cows = await _repository.GetAllCows(GetUserId());
            serviceResponse.Value = cows.Select(c => _mapper.Map<GetCowDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCowDto>> GetCowById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCowDto>();    
            var cow = await _repository.GetCowById(id);
            if(cow is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Cow with ID:{id} not found.";
                return serviceResponse;

            }
            serviceResponse.Value = _mapper.Map<GetCowDto>(cow);
            serviceResponse.Message = "Cow data fetched successfully.";
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCowDto>> UpdateCow(UpdateCowDto updatedCow)
        {
            var serviceResponse = new ServiceResponse<GetCowDto>();

            try
            {
                var cow = await _repository.GetCowById(updatedCow.Id);
                if (cow == null) throw new Exception($"Cow with Id '{updatedCow.Id}' not found.");
                cow.Name = updatedCow.Name;
                cow.Breed = updatedCow.Breed;
                cow.Age = updatedCow.Age;

                await _repository.UpdateCow(cow);

                serviceResponse.Value = _mapper.Map<GetCowDto>(cow);
                

            }
            catch(Exception ex) 
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
