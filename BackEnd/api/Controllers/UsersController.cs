using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.User;
using api.Interfaces;
using api.Interfaces.Services;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService userRepository)
        {
            _usersService = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok( await _usersService.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var user = await _usersService.GetByIdAsync(id);
            if(user == null) return NotFound();

            return Ok(user.ToUserDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequestDto userDto)
        {
            var userModel = await _usersService.CreateAsync(userDto);

            return Ok(userModel);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserRequestDto userDto)
        {
            var user = await _usersService.UpdateAsync(id,userDto);

            if (user == null) return NotFound("Usuário não encontrado");

            return Ok(new { message = "Usuário atualizado com sucesso" });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var user = await _usersService.DeleteAsync(id);

            if (user == null) return NotFound("Usuário não encontrado");

            return NoContent();
        }
    }
}
