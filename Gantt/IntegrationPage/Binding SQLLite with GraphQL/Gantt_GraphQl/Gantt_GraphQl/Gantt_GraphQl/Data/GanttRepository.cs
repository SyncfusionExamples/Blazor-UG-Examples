using Gantt_GraphQl.Model;
using Microsoft.EntityFrameworkCore;

namespace Gantt_GraphQl.Data
{
    public class GanttRepository
    {
        private readonly GanttDbContext _db;
        public GanttRepository(GanttDbContext db) => _db = db;

        public IQueryable<TaskEntity> Query() => _db.Tasks.AsNoTracking();
        public async Task<List<TaskEntity>> GetAllAsync() => await _db.Tasks.OrderBy(t => t.TaskId).ToListAsync();
        public async Task<int> InsertAsync(TaskEntity t) { _db.Tasks.Add(t); await _db.SaveChangesAsync(); return t.TaskId; }
        public async Task UpdateAsync(TaskEntity t) { _db.Tasks.Update(t); await _db.SaveChangesAsync(); }
        public async Task DeleteAsync(int id) { var e = await _db.Tasks.FindAsync(id); if (e != null) { _db.Tasks.Remove(e); await _db.SaveChangesAsync(); } }
    }
}
