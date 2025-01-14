using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.User;
using api.Models;

namespace api.Interfaces
{
    public interface IUsersRepository
    {
        Task<List<Users>> GetAllAsync();
        Task<Users?> GetByIdAsync(int id);
        Task<Users?> GetByNameAsync(string name);

        Task<Users> CreateAsync(Users userModel);
        Task<Users?> DeleteAsync(Users user);
        Task SaveAsync();
    }
}