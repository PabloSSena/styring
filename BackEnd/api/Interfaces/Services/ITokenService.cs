using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateToken(Users users);
    }
}