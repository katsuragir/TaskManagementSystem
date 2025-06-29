using Application.DTOs;
using Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;
        private readonly IValidator<CreateTaskRequest> _validator;

        public TasksController(TaskService taskService, IValidator<CreateTaskRequest> validator)
        {
            _taskService = taskService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _taskService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _taskService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("/api/users/{userId}/tasks")]
        public async Task<IActionResult> GetByUser(Guid userId)
        {
            var result = await _taskService.GetByAssigneeAsync(userId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var created = await _taskService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskRequest request)
        {
            var updated = await _taskService.UpdateAsync(id, request);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _taskService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        [HttpPatch("{taskId}/assign/{userId}")]
        public async Task<IActionResult> AssignTaskToUser(Guid taskId, Guid userId)
        {
            var updated = await _taskService.UpdateAsync(taskId, new UpdateTaskRequest { AssigneeUserId = userId });
            return updated ? Ok(new { taskId, assignedTo = userId }) : NotFound();
        }
    }
}
