using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.User;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDBContext _database;
        public UsersRepository(ApplicationDBContext database)
        {
            _database = database;
        }

        public async Task<Users> CreateAsync(Users userModel)
        {
            await _database.Users.AddAsync(userModel);
            await _database.SaveChangesAsync();
            return userModel;

        }

        public async Task<Users?> DeleteAsync(Users user)
        {
            _database.Users.Remove(user);
            await _database.SaveChangesAsync();

            return user;
        }

        public async Task<List<Users>> GetAllAsync()
        {
            return await _database.Users.ToListAsync();
        }

        public async Task<Users?> GetByIdAsync(int id)
        {
            return await _database.Users.FindAsync(id);
        }

        public async Task<Users?> GetByNameAsync(string name)
        {
            return await _database.Users.FirstOrDefaultAsync(user => user.Name == name);
        }

        public async Task SaveAsync()
        {
            await _database.SaveChangesAsync();
        }
    }
}