using FarmInfo.Models;

namespace FarmInfo.Repositories.AuthRepo
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(Farmer farmer, string password);
        Task<ServiceResponse<string>> Login(string username, string password); 
        Task<bool> UserExists(string username); // Will not be sended client
    }
}
