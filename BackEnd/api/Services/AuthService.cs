using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using api.Dtos.Login;
using api.Interfaces;
using api.Interfaces.Services;

namespace api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsersRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IUsersRepository userRepository,ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<LoginReponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByNameAsync(loginDto.Name);

            if(user == null) throw new UnauthorizedAccessException("Invalid user");

            if(!VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
                throw new UnauthorizedAccessException("Invalid user");

            return new LoginReponseDto{
                Token = _tokenService.GenerateToken(user),
                UserId = user.Id.ToString()
            };
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }
    }
}