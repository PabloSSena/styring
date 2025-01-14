using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.User;
using api.Models;

namespace api.Mappers
{
    public static class UsersMappers
    {
        public static UsersDto ToUserDto(this Users userModel){
            return new UsersDto
            {
                Id = userModel.Id,
                Name = userModel.Name
            };
        }

        public static Users ToUserFromCreateDto(this CreateUserRequestDto createUserRequestDto, byte[] passwordHash, byte[] passwordSalt)
        {
            return new Users
            {
                Name = createUserRequestDto.Name,
                Password = createUserRequestDto.Password,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
        }
    }
}