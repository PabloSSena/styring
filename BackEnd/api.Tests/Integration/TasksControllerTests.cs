using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using api.Dtos.Task;
using api.Models;
using api.Tests.Factories;
using Azure;
using Newtonsoft.Json;

namespace api.Tests.Integration
{
    public class TasksControllerTests :IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public TasksControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateTask_ShouldReturnSucces()
        {
            var newUser = new 
            {
                Name = "userTest",
                Password = "passwordTest"
            };

            var userToJson = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/users", userToJson);
            
            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<Users>();


                var taskDto = new TaskDto
                {
                    Title = "Nova Tarefa",
                    Description = "Descrição da tarefa",
                    Status = "Pendente",
                    UserId = user.Id,
                    DueDate = DateOnly.FromDateTime(DateTime.Now)
                };

                var taskToJson = new StringContent(JsonConvert.SerializeObject(taskDto), Encoding.UTF8, "application/json");

                var responseTasks = await _client.PostAsync("/api/tasks", taskToJson);

                Assert.Equal(HttpStatusCode.OK, responseTasks.StatusCode); 
            }
        
        }

        [Fact]
        public async Task ListTask_ShouldReturnSucces()
        {
            var newUser = new 
            {
                Name = "userTest",
                Password = "passwordTest"
            };

            var userToJson = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/users", userToJson);
            
            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<Users>();


                var taskDto = new TaskDto
                {
                    Title = "Nova Tarefa",
                    Description = "Descrição da tarefa",
                    Status = "Pendente",
                    UserId = user.Id,
                    DueDate = DateOnly.FromDateTime(DateTime.Now)
                };

                var taskToJson = new StringContent(JsonConvert.SerializeObject(taskDto), Encoding.UTF8, "application/json");

                await _client.PostAsync("/api/tasks", taskToJson);

                var responseGetTasks = await _client.GetAsync($"/api/tasks/{user.Id}");


                Assert.Equal(HttpStatusCode.OK, responseGetTasks.StatusCode); 
            }
        
        }
        
    }
}