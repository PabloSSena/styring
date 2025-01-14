using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.User
{
    public class UpdateUserRequestDto
    {
        public string Name {get;set;} = string.Empty;
        public string Password {get;set;} = string.Empty;
    }
}