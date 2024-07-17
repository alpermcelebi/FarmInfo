using FarmInfo.Dtos.CowDtos;
using FarmInfo.Models;

namespace FarmInfo.Services.CowService
{
    public interface ICowService
    {
        Task<ServiceResponse<List<GetCowDto>>> GetAllCows();
        Task<ServiceResponse<GetCowDto>> GetCowById(int id);
        Task<ServiceResponse<List<GetCowDto>>> AddCow(AddCowDto newCow);
        Task<ServiceResponse<GetCowDto>> UpdateCow(UpdateCowDto updatedCow);
        Task<ServiceResponse<List<GetCowDto>>> DeleteCow(int id);

    }
}
