using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using api.Models;
using api.Tests.Factories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
namespace api.Tests.Integration
{
    public class UserControllerTests: IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        public UserControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }
        
        [Fact]
        public async Task CreateUser_ShouldReturnSuccess()
        {
            
            var newUser = new 
            {
                Name = "userTest",
                Password = "passwordTest"
            };

            var userToJson = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/users", userToJson);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturnSuccess()
        {

            var response = await _client.GetAsync("/api/users");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task FindUserById_ShouldReturnSuccess()
        {
            var newUser = new 
            {
                Name = "userTest",
                Password = "passwordTest"
            };

            var userToJson = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");

            var createdUser = await _client.PostAsync("/api/users", userToJson);
            createdUser.EnsureSuccessStatusCode();

            var convertedUser = JsonConvert.DeserializeObject<Users>(
                await createdUser.Content.ReadAsStringAsync()
            );

            int userId = convertedUser.Id;

            var response = await _client.GetAsync($"/api/users/{userId}");

            var retrievedUser = JsonConvert.DeserializeObject<Users>(
                await response.Content.ReadAsStringAsync()
            );

            Assert.Equal(userId, retrievedUser.Id);
        }

    }
    

}