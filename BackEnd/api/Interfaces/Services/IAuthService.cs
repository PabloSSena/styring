using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Login;

namespace api.Interfaces.Services
{
    public interface IAuthService
    {
        Task<LoginReponseDto> LoginAsync(LoginDto loginDto);
    }
}