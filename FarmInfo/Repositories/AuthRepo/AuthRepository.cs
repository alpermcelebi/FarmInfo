using FarmInfo.Models;
using FarmInfo.Repositories.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FarmInfo.Repositories.AuthRepo
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();
            var farmer = await _context.Farmers.FirstOrDefaultAsync(f => f.Username.ToLower().Equals(username.ToLower()));
            if (farmer == null)
            {
                response.Success = false;
                response.Message = "Farmer with this username not found.";
            }
            else if (VerifyPasswordHash(password, farmer.PasswordHash, farmer.PasswordSalt) == false)
            {
                response.Success = false;
                response.Message = "Wrong password!";
            }
            else
            {
                response.Value = CreateToken(farmer);
                response.Message = "Logged in successfully.";
            }
            return response;
        }

        public async Task<ServiceResponse<int>> Register(Farmer farmer, string password)
        {
            var response = new ServiceResponse<int>();
            if (await UserExists(farmer.Username))
            {
                response.Success = false;
                response.Message = "Farmer with this username already exists. Please try with another username.";
                return response;
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            farmer.PasswordHash = passwordHash;
            farmer.PasswordSalt = passwordSalt;

            _context.Farmers.Add(farmer);
            await _context.SaveChangesAsync();

            response.Value = farmer.Id;
            response.Message = "Registration has been done successfully.";
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _context.Farmers.AnyAsync(f => f.Username.ToLower() == username.ToLower());
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(Farmer farmer)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, farmer.Id.ToString()),
                new Claim(ClaimTypes.Name, farmer.Username),
            };

            var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;
            if (appSettingsToken == null)
            {
                throw new Exception("AppSetting Token is null!");
            }

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
