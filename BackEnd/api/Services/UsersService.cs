using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Repository;
using api.Interfaces.Services;
using api.Models;
using api.Dtos.User;
using Microsoft.AspNetCore.Http.HttpResults;
using api.Mappers;
using api.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace api.Services
{
    public class UsersService: IUsersService 
    {   
        private IUsersRepository _usersRepository; 
        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<UsersDto> CreateAsync(CreateUserRequestDto userDto)
        {
            CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUser = userDto.ToUserFromCreateDto(passwordHash,passwordSalt);

            var created = await  _usersRepository.CreateAsync(newUser);

            return created.ToUserDto();
        }

        public async Task<Users?> DeleteAsync(int id)
        {
            var user = await _usersRepository.GetByIdAsync(id);
            if(user == null) return null;

            return await _usersRepository.DeleteAsync(user);
        }

        public async Task<List<UsersDto>> GetAllAsync()
        {
            var users = await _usersRepository.GetAllAsync();
            return users.Select(user => user.ToUserDto()).ToList();
        }

        public async Task<Users?> GetByIdAsync(int id)
        {
            return await _usersRepository.GetByIdAsync(id);
        }

        public async Task<Users?> UpdateAsync(int id, UpdateUserRequestDto userDto)
        {
        var existingUser = await _usersRepository.GetByIdAsync(id);

        if(existingUser == null) return null;

        existingUser.Name = userDto.Name;
        existingUser.Password = userDto.Password;

        await _usersRepository.SaveAsync();
        return existingUser;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}