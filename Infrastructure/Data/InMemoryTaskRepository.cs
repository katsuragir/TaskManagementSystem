using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class InMemoryTaskRepository : ITaskRepository
    {
        private readonly List<TaskItem> _tasks = new();

        public Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return Task.FromResult(_tasks.AsEnumerable());
        }

        public Task<IEnumerable<TaskItem>> GetByAssigneeAsync(Guid userId)
        {
            var result = _tasks.Where(t => t.AssigneeUserId == userId);
            return Task.FromResult(result);
        }

        public Task<TaskItem?> GetByIdAsync(Guid id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            return Task.FromResult(task);
        }

        public Task AddAsync(TaskItem task)
        {
            _tasks.Add(task);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(TaskItem task)
        {
            var index = _tasks.FindIndex(t => t.Id == task.Id);
            if (index >= 0)
                _tasks[index] = task;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
                _tasks.Remove(task);
            return Task.CompletedTask;
        }
    }
}
