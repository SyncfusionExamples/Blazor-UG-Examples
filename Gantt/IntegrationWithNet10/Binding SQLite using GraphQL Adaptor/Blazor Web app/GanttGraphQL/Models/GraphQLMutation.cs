using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace GanttGraphQL.Models
{
    /// <summary>
    /// GraphQL mutation resolvers for Syncfusion Blazor Gantt.
    /// Provides create, update, delete, and batch-update operations for tasks.
    /// </summary>
    public class GraphQLMutation
    {
        /// <summary>
        /// Creates a new task record and persists it to the database.
        /// Applies defaults for missing values (TaskName, StartDate, Duration, Progress).
        /// </summary>
        /// <param name="record">The task payload received from the client.</param>
        /// <param name="index">The target insertion index (unused for EF set ordering).</param>
        /// <param name="action">The action name sent by the client (e.g., "insert").</param>
        /// <param name="additionalParameters">Additional custom parameters sent from the client.</param>
        /// <param name="db">The EF Core database context.</param>
        /// <returns>The created <see cref="TaskDataModel"/> entity.</returns>
        public TaskDataModel CreateTask(TaskDataModel record, int index, string action, [GraphQLType(typeof(AnyType))] IDictionary<string, object> additionalParameters, [Service] GanttDbContext db)
        {
            TaskDataModel? entity = new TaskDataModel
            {
                TaskName = string.IsNullOrWhiteSpace(record.TaskName) ? "New Task" : record.TaskName!,
                StartDate = record.StartDate ?? DateTime.Now,
                EndDate = record.EndDate,
                Duration = record.Duration ?? "1",
                Progress = record.Progress ?? 0,
                Predecessor = record.Predecessor,
                ParentID = record.ParentID
            };

            db.Tasks.Add(entity);
            db.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Updates an existing task by primary key and persists the changes.
        /// </summary>
        /// <param name="record">The incoming task payload with updated values.</param>
        /// <param name="action">The action name sent by the client (e.g., "update").</param>
        /// <param name="primaryColumnName">The primary key column name (e.g., "TaskID").</param>
        /// <param name="primaryColumnValue">The primary key value of the task to update.</param>
        /// <param name="additionalParameters">Additional custom parameters sent from the client.</param>
        /// <param name="db">The EF Core database context.</param>
        /// <returns>The updated <see cref="TaskDataModel"/> entity.</returns>        
        public TaskDataModel UpdateTask(TaskDataModel record, string action, string primaryColumnName, int primaryColumnValue, [GraphQLType(typeof(AnyType))] IDictionary<string, object> additionalParameters, [Service] GanttDbContext db)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record), "Task cannot be null");

            TaskDataModel? previousRecord = db.Tasks.FirstOrDefault(x => x.TaskID == primaryColumnValue);

            previousRecord.TaskName = record.TaskName;
            previousRecord.StartDate = record.StartDate;
            previousRecord.EndDate = record.EndDate;
            previousRecord.Duration = record.Duration;
            previousRecord.Progress = record.Progress;
            previousRecord.Predecessor = record.Predecessor;
            previousRecord.ParentID = record.ParentID;

            db.SaveChangesAsync();
            return previousRecord;
        }

        /// <summary>
        /// Deletes a task by its key.
        /// </summary>
        /// <param name="key">The primary key value of the task to delete.</param>
        /// <param name="additionalParameters">Additional custom parameters sent from the client.</param>
        /// <param name="db">The EF Core database context.</param>
        /// <returns>
        /// <see langword="true"/> if the task was found and removed; otherwise, <see langword="false"/>.
        /// </returns>
        public bool DeleteTask([ID][GraphQLName("key")] int key, [GraphQLType(typeof(AnyType))] IDictionary<string, object> additionalParameters, [Service] GanttDbContext db)
        {
            TaskDataModel record = db.Tasks.FirstOrDefault(t => t.TaskID == key);
            if (record == null) return false;

            db.Tasks.Remove(record);
            db.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Applies batch operations (update, add, delete) to tasks in a single request and saves changes.
        /// </summary>
        /// <param name="changed">The collection of modified task records to update.</param>
        /// <param name="added">The collection of new task records to insert.</param>
        /// <param name="deleted">The collection of task records to remove.</param>
        /// <param name="action">The batch action name sent by the client.</param>
        /// <param name="primaryColumnName">The primary key column name (e.g., "TaskID").</param>
        /// <param name="additionalParameters">Additional custom parameters sent from the client.</param>
        /// <param name="dropIndex">Target insert index (not applied unless you maintain an order column).</param>
        /// <param name="db">The EF Core database context.</param>
        /// <returns>The full list of tasks after applying the batch update.</returns>        
        public List<TaskDataModel> BatchUpdate(List<TaskDataModel>? changed, List<TaskDataModel>? added, List<TaskDataModel>? deleted, string action, string primaryColumnName, [GraphQLType(typeof(AnyType))] IDictionary<string, object> additionalParameters, int? dropIndex, [Service] GanttDbContext db)
        {
            // Update existing tasks
            if (changed != null)
            {
                foreach (TaskDataModel task in changed)
                {
                    TaskDataModel record = db.Tasks.FirstOrDefault(t => t.TaskID == task.TaskID);
                    if (record != null)
                    {
                        record.TaskName = string.IsNullOrWhiteSpace(task.TaskName) ? record.TaskName : task.TaskName!;
                        record.StartDate = task.StartDate ?? record.StartDate;
                        record.EndDate = task.EndDate;
                        record.Duration = task.Duration ?? record.Duration;
                        record.Progress = task.Progress ?? record.Progress;
                        record.Predecessor = task.Predecessor;
                        record.ParentID = task.ParentID;
                    }
                }
            }

            // Add new tasks
            if (added != null)
            {
                foreach (TaskDataModel task in added)
                {
                    TaskDataModel record = new TaskDataModel
                    {
                        // TaskID is assumed to be identity; do not set it
                        TaskName = string.IsNullOrWhiteSpace(task.TaskName) ? "New Task" : task.TaskName!,
                        StartDate = task.StartDate ?? DateTime.Now,
                        EndDate = task.EndDate,
                        Duration = task.Duration ?? "1",
                        Progress = task.Progress ?? 0,
                        Predecessor = task.Predecessor,
                        ParentID = task.ParentID
                    };
                    db.Tasks.Add(record);
                }
            }

            // Delete tasks
            if (deleted != null)
            {
                foreach (TaskDataModel task in deleted)
                {
                    TaskDataModel record = db.Tasks.FirstOrDefault(t => t.TaskID == task.TaskID);
                    if (record != null)
                    {
                        db.Tasks.Remove(record);
                    }
                }
            }

            db.SaveChanges();
            return db.Tasks.ToList();
        }
    }
}
