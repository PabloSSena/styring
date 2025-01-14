using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Task;
using api.Helpers;
using api.Models;
namespace api.Interfaces.Repositories
{
    public interface ITasksRepository
    {
        Task<Tasks> CreateTask(Tasks task);
        Task<List<Tasks>> GetTasksByUserId(int id, QueryObject queryObject);
        Task<Tasks> GetTasksById(int id);
        Task<Tasks?> DeleteTask(Tasks task);
        Task SaveAsync();
    }
}