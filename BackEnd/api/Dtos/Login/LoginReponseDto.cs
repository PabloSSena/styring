using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Login
{
    public class LoginReponseDto
    {
    public string Token { get; set; } = String.Empty;
    public string UserId { get; set; } = String.Empty;
    }
}