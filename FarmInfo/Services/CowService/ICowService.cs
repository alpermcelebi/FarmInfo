using FarmInfo.Dtos.CowDtos;
using FarmInfo.Models;

namespace FarmInfo.Services.CowService
{
    public interface ICowService
    {
        Task<Response<List<GetCowDto>>> GetAllCows();
        Task<Response<GetCowDto>> GetCowById(int id);
        Task<Response<List<GetCowDto>>> AddCow(AddCowDto newCow);
        Task<Response<GetCowDto>> UpdateCow(UpdateCowDto updatedCow);
        Task<Response<List<GetCowDto>>> DeleteCow(int id);

    }
}
