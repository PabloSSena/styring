using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Task;
using api.Models;

namespace api.Mappers
{
    public static class TasksMapper
    {
        public static Tasks ToTaskFromCreateDto(this TaskDto createTaskDto)
        {
            return new Tasks
            {
                Description = createTaskDto.Description,
                DueDate = createTaskDto.DueDate,
                Status = createTaskDto.Status,
                Title = createTaskDto.Title,
                UserId = createTaskDto.UserId
            };
        }

        public static TaskResponseDto ToTaskResponseDto(this Tasks createTaskDto)
        {
            return new TaskResponseDto
            {
                Id = createTaskDto.Id,
                Description = createTaskDto.Description,
                DueDate = createTaskDto.DueDate,
                Status = createTaskDto.Status,
                Title = createTaskDto.Title,
                
            };
        }
    }
}