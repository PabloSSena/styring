using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Task;
using api.Helpers;
using api.Interfaces.Services;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _tasksService;

        public TasksController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskDto taskDto)
        {
            var taskModel = await _tasksService.CreateTask(taskDto);
            return Ok(taskModel);
        }

        [HttpGet("{userId:int}")]
        public async Task<ActionResult> GetById([FromRoute] int userId, [FromQuery] QueryObject query)
        {
            var tasks = await _tasksService.GetTasksByUserId(userId, query);
            return Ok(tasks);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTaskDto taskDto)
        {
            var update = await _tasksService.UpdateTask(id,taskDto);
            if(update == null) return NotFound();

            return Ok(update);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var delete = await _tasksService.DeleteTask(id);
            if(delete == null) return NotFound();

            return Ok(delete);
        }
    }
}