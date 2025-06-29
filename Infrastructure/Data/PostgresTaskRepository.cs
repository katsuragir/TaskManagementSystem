using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class PostgresTaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _db;

        public PostgresTaskRepository(TaskDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync() =>
            await _db.Tasks.ToListAsync();

        public async Task<TaskItem?> GetByIdAsync(Guid id) =>
            await _db.Tasks.FindAsync(id);

        public async Task<IEnumerable<TaskItem>> GetByAssigneeAsync(Guid userId) =>
            await _db.Tasks.Where(t => t.AssigneeUserId == userId).ToListAsync();

        public async Task AddAsync(TaskItem task)
        {
            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskItem task)
        {
            _db.Tasks.Update(task);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var task = await _db.Tasks.FindAsync(id);
            if (task != null)
            {
                _db.Tasks.Remove(task);
                await _db.SaveChangesAsync();
            }
        }
    }
}
