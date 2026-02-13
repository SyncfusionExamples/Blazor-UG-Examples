using GanttMySql.Data;
using Microsoft.EntityFrameworkCore;

namespace GanttMySql.Data
{
    /// <summary>
    /// Repository implementation for TaskDataModel using EF Core.
    /// Handles CRUD operations and simple task business rules.
    /// </summary>
    public class TaskRepository
    {
        private readonly TaskDbContext _context;

        /// <summary>
        /// Initializes a new instance of <see cref="TaskRepository"/>.
        /// </summary>
        /// <param name="context">The <see cref="TaskDbContext"/> used to access the database.</param>
        public TaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all tasks ordered for hierarchical display.
        /// </summary>
        /// <returns>A list of <see cref="TaskDataModel"/> instances.</returns>
        public async Task<List<TaskDataModel>> GetTasksAsync()
        {
            try
            {
                return await _context.TaskData
                    .OrderBy(t => t.ParentId == null ? 0 : 1) 
                    .ThenBy(t => t.ParentId)                  
                    .ThenBy(t => t.TaskId)                    
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
        /// <param name="task">The task to add.</param>
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
        /// <param name="task">Updated task data (TaskId must identify an existing task).</param>
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
            existing.Predecessor = task.Predecessor;
            existing.ParentId = task.ParentId;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a task by TaskId.
        /// </summary>
        /// <param name="key">Task identifier to remove; null or invalid values are ignored.</param>
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

        /// <summary>
        /// Applies default values and enforces simple business rules on a task instance.
        /// </summary>
        /// <param name="task">Task instance to modify in-place.</param>
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

