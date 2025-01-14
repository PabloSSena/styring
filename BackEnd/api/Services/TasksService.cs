using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Task;
using api.Helpers;
using api.Interfaces.Repositories;
using api.Interfaces.Services;
using api.Mappers;
using api.Models;

namespace api.Services
{
    public class TasksService : ITasksService
    {
        private ITasksRepository _tasksRepository;
        public TasksService(ITasksRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }       

        public async Task<Tasks> CreateTask(TaskDto taskDto)
        {
            var task = taskDto.ToTaskFromCreateDto();
            return await _tasksRepository.CreateTask(task);
        }
        public async Task<List<TaskResponseDto>> GetTasksByUserId(int id, QueryObject queryObject)
        {
            var taskResponse = await _tasksRepository.GetTasksByUserId(id, queryObject);
            var taskResponseDtos = taskResponse.Select(test => test.ToTaskResponseDto()).ToList();

            return taskResponseDtos;
        }

        public async Task<Tasks> UpdateTask(int id,UpdateTaskDto taskDto)
        {
            var existingTask = await _tasksRepository.GetTasksById(id);

            if(existingTask == null) return null;

            existingTask.Title = taskDto.Title;
            existingTask.Description = taskDto.Description;
            existingTask.Status = taskDto.Status;
            existingTask.DueDate = taskDto.DueDate;
            
            await _tasksRepository.SaveAsync();
            return existingTask;
        }

        public async Task<Tasks?> DeleteTask(int id)
        {
            var task = await _tasksRepository.GetTasksById(id);
            if(task == null) return null;

            return await _tasksRepository.DeleteTask(task);
        }

    }
}