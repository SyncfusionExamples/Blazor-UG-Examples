using Microsoft.EntityFrameworkCore;

namespace Gantt_MySQL.Data
{

    /// <summary>
    /// Repository implementation for TaskDataModel using EF Core.
    /// Handles CRUD operations and simple task business rules.
    /// </summary>
    public class TaskRepository
    {
        private readonly TaskDbContext _context;       

        public TaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all tasks ordered by TaskId descending.
        /// </summary>
        public async Task<List<TaskDataModel>> GetTasksAsync()
        {
            try
            {
                return await _context.TaskData
                    .OrderByDescending(t => t.TaskId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving tasks: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Adds a new task to the database with defaults and validation.
        /// </summary>
        public async Task AddTaskAsync(TaskDataModel task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task), "Task cannot be null");

            // Ensure DB generates identity
            task.TaskId = 0;

            ApplyDefaults(task);

            _context.TaskData.Add(task);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing task in the database.
        /// </summary>
        public async Task UpdateTaskAsync(TaskDataModel task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task), "Task cannot be null");

            var existing = await _context.TaskData.FindAsync(task.TaskId);
            if (existing == null)
                throw new KeyNotFoundException($"Task with ID {task.TaskId} not found in the database.");

            ApplyDefaults(task);

            existing.TaskName = task.TaskName;
            existing.StartDate = task.StartDate;
            existing.EndDate = task.EndDate;
            existing.Duration = task.Duration;
            existing.Progress = task.Progress;
            existing.ParentId = task.ParentId;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a task by TaskId.
        /// </summary>
        public async Task RemoveTaskAsync(int? key)
        {
            if (key == null || key <= 0)
                return; // don’t throw for invalid key in UI flows

            try
            {
                var task = await _context.TaskData.FindAsync(key.Value);
               
                if (task == null)
                    return;

                _context.TaskData.Remove(task);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database error while deleting task: {ex.Message}");
                throw;
            }
        }

        private static void ApplyDefaults(TaskDataModel task)
        {
            task.TaskName = string.IsNullOrWhiteSpace(task.TaskName) ? "New Task" : task.TaskName.Trim();
            task.StartDate ??= DateTime.Now;

            if (string.IsNullOrWhiteSpace(task.Duration))
                task.Duration = "1 day"; // or "1d"

            // Clamp progress 0..100
            if (task.Progress < 0) task.Progress = 0;
            if (task.Progress > 100) task.Progress = 100;

            if (task.EndDate != null && task.StartDate != null && task.EndDate < task.StartDate)
                task.EndDate = task.StartDate.Value.AddDays(1);
        }
    }
}

