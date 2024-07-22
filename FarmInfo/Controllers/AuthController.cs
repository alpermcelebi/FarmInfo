using FarmInfo.Dtos.FarmerDtos;
using FarmInfo.Models;
using FarmInfo.Repositories.AuthRepo;
using Microsoft.AspNetCore.Mvc;

namespace FarmInfo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
     
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(FarmerRegisterDto request)
        {
            var response = await _authRepository.Register(
                new Farmer { Username = request.Username }, request.Password);
            if(response.Success == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> login(FarmerLoginDto request)
        {
            var response = await _authRepository.Login(request.Username, request.Password);
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            
            return Ok(response);
        }
    }
}
