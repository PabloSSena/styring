using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.User;
using api.Models;

namespace api.Interfaces.Services
{
    public interface IUsersService
    {
        Task<List<UsersDto>> GetAllAsync();
        Task<Users?> GetByIdAsync(int id);
        Task<UsersDto> CreateAsync(CreateUserRequestDto userModel);
        Task<Users?> UpdateAsync(int id, UpdateUserRequestDto userDto);
        Task<Users?> DeleteAsync(int id);
        
    }
}