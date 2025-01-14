using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Task;
using api.Helpers;
using api.Models;

namespace api.Interfaces.Services
{
    public interface ITasksService
    {
        Task<Tasks> CreateTask(TaskDto task);
        Task<List<TaskResponseDto>> GetTasksByUserId(int id, QueryObject queryObject);
        Task<Tasks> UpdateTask(int id, UpdateTaskDto taskDto);
        Task<Tasks?> DeleteTask(int id);
    }
}