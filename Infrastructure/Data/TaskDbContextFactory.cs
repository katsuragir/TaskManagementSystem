using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data
{
    public class TaskDbContextFactory : IDesignTimeDbContextFactory<TaskDbContext>
    {
        public TaskDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TaskDbContext>();
            optionsBuilder.UseNpgsql("Host=db.ktniekhdyomjntrutbmv.supabase.co;Port=5432;Database=postgres;Username=postgres;Password=subagioAQ123!;SslMode=Require;Trust Server Certificate=true");

            return new TaskDbContext(optionsBuilder.Options);
        }
    }
}
