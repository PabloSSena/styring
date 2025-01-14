using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Task;
using api.Helpers;
using api.Interfaces.Repositories;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class TasksRepository : ITasksRepository
    {
        private readonly ApplicationDBContext _database;

        public TasksRepository(ApplicationDBContext database)
        {
            _database = database;
        }
        public async Task<Tasks> CreateTask(Tasks task)
        {
            await _database.Tasks.AddAsync(task);
            await _database.SaveChangesAsync();
            return task;
        }

        public async Task<List<Tasks>> GetTasksByUserId(int id, QueryObject queryObject)
        {            
            var query = _database.Tasks.Where(task => task.UserId == id);

            if(!string.IsNullOrEmpty(queryObject.Status))
            {
                query = query.Where(task => task.Status == queryObject.Status);
            }

            if(!string.IsNullOrEmpty(queryObject.DueDate))
            {   
                Console.WriteLine($"queryObject.dueDate: {queryObject.DueDate}");
                query = query.Where(task => task.DueDate.ToString() == queryObject.DueDate);
            }

            if (!string.IsNullOrEmpty(queryObject.Search))
            {
                string searchTerm = queryObject.Search.ToLower();
                query = query.Where(task =>
                task.Title.ToLower().Contains(searchTerm) || task.Description.ToLower().Contains(searchTerm));
            }
            
            query = query.OrderBy(task => task.DueDate);
            

            return await query.ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _database.SaveChangesAsync();
        }

        public async Task<Tasks> GetTasksById(int id)
        {
            return await _database.Tasks.FindAsync(id);
        }
        public async Task<Tasks?> DeleteTask(Tasks task)
        {
            _database.Tasks.Remove(task);
            await _database.SaveChangesAsync();

            return task;
        }
    }
}