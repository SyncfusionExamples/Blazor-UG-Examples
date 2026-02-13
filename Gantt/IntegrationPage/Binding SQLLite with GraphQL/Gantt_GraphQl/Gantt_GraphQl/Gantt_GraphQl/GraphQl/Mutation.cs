using Gantt_GraphQl.Data;
using Gantt_GraphQL.GraphQl;
using Gantt_GraphQl.Model;
using Microsoft.EntityFrameworkCore;

namespace Gantt_GraphQl.GraphQl
{
    public class Mutation
    {
        public async Task<TaskEntity> InsertTask(
            TaskInput record,
            int position,
            string action,
            object? additionalParameters,
            [Service] GanttDbContext db)
        {
            var entity = new TaskEntity
            {
                TaskName = string.IsNullOrWhiteSpace(record.TaskName) ? "New Task" : record.TaskName!,
                StartDate = record.StartDate ?? DateTime.Now,
                EndDate = record.EndDate,
                ParentId = record.ParentId,
                Duration = record.Duration ?? 1,
                Progress = record.Progress ?? 0
            };

            db.Tasks.Add(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public async Task<TaskEntity> UpdateTask(
            TaskInput record,
            string action,
            string keyField,
            int keyValue,
            object? additionalParameters,
            [Service] GanttDbContext db)
        {
            var entity = await db.Tasks.FirstOrDefaultAsync(t => t.TaskId == keyValue);
            if (entity == null)
                throw new GraphQLException($"TaskId {keyValue} not found.");

            entity.TaskName = string.IsNullOrWhiteSpace(record.TaskName) ? entity.TaskName : record.TaskName!;
            entity.StartDate = record.StartDate ?? entity.StartDate;
            entity.EndDate = record.EndDate;
            entity.ParentId = record.ParentId;
            entity.Duration = record.Duration ?? entity.Duration;
            entity.Progress = record.Progress ?? entity.Progress;

            await db.SaveChangesAsync();
            return entity;
        }

        public async Task<TaskEntity> DeleteTask(
            int id,
            string action,
            string keyField,
            object? additionalParameters,
            [Service] GanttDbContext db)
        {
            var entity = await db.Tasks.FirstOrDefaultAsync(t => t.TaskId == id);
            if (entity == null)
                throw new GraphQLException($"TaskId {id} not found.");

            db.Tasks.Remove(entity);
            await db.SaveChangesAsync();
            return entity;
        }
    }
}