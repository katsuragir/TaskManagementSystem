using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _repository;
        private readonly ILoggerService _logger;

        public TaskService(ITaskRepository repository, ILoggerService logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<TaskResponse>> GetAllAsync()
        {
            var tasks = await _repository.GetAllAsync();
            _logger.LogInfo("Retrieved all tasks");
            return tasks.Select(MapToResponse);
        }

        public async Task<IEnumerable<TaskResponse>> GetByAssigneeAsync(Guid userId)
        {
            var tasks = await _repository.GetByAssigneeAsync(userId);
            _logger.LogInfo($"Retrieved tasks for user {userId}");
            return tasks.Select(MapToResponse);
        }

        public async Task<TaskResponse?> GetByIdAsync(Guid id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task != null)
                _logger.LogInfo($"Retrieved task with ID {id}");
            return task != null ? MapToResponse(task) : null;
        }

        public async Task<TaskResponse> CreateAsync(CreateTaskRequest request)
        {
            var task = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                Priority = request.Priority,
                Status = request.Status,
                AssigneeUserId = request.AssigneeUserId
            };

            await _repository.AddAsync(task);
            _logger.LogInfo($"Created new task with ID {task.Id}");
            return MapToResponse(task);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateTaskRequest request)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
            {
                _logger.LogWarning($"Task with ID {id} not found for update");
                return false;
            }

            if (request.Title != null) existing.Title = request.Title;
            if (request.Description != null) existing.Description = request.Description;
            if (request.DueDate.HasValue) existing.DueDate = request.DueDate.Value;
            if (request.Priority != null) existing.Priority = request.Priority;
            if (request.Status != null) existing.Status = request.Status;
            if (request.AssigneeUserId.HasValue) existing.AssigneeUserId = request.AssigneeUserId;

            await _repository.UpdateAsync(existing);
            _logger.LogInfo($"Updated task with ID {id}");
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
            {
                _logger.LogWarning($"Task with ID {id} not found for deletion");
                return false;
            }
            await _repository.DeleteAsync(id);
            _logger.LogInfo($"Deleted task with ID {id}");
            return true;
        }

        private TaskResponse MapToResponse(TaskItem task)
        {
            return new TaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Priority = task.Priority,
                Status = task.Status,
                AssigneeUserId = task.AssigneeUserId
            };
        }
    }
}
