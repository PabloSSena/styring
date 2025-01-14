using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Tests.Domain.Entities.User
{
    public class UserTest
    {
        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "User")]
        public void Instantiate()
        {
            var dataToTest = new
            {
                Name = "usuário",
                Password = "Senha"

            };

            // var user = new Users(dataToTest.Name,dataToTest.Password);

            // Assert.NotNull(user);
            // Assert.Equal(dataToTest.Name, user.Name);
            // Assert.Equal(dataToTest.Password, user.Password);

        }
    }
}