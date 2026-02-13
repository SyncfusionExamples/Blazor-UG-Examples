using Gantt_GraphQl.Data;
using Gantt_GraphQl.Model;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;
using System.Text.Json;

namespace Gantt_GraphQl.GraphQl
{
    /// <summary>
    /// GraphQL Query resolver for Syncfusion GraphQLAdaptor.
    /// Must return an object with "count" and "result". [1](https://github.com/SyncfusionExamples/GraphQLAdaptor-Binding-Blazor-DataGrid)[2](https://blazor.syncfusion.com/documentation/data/getting-started)
    /// </summary>
    public class GanttQuery
    {
        /// <summary>
        /// IMPORTANT:
        /// - GraphQL field name is forced to "taskData" so it matches ResolverName="taskData" in the client.
        /// - Accepts Syncfusion DataManagerRequest directly so DataOperations works without type conversion.
        /// </summary>
        [GraphQLName("taskData")]
        public async Task<TaskDataResponse> TaskData(
            [GraphQLName("dataManager")] DataRequest dataManagerInput,
            [Service] GanttDbContext db)
        {
            dataManagerInput ??= new DataRequest { Skip = 0, Take = 0, RequiresCounts = false };

            // Map simple scalar properties to Syncfusion DataManagerRequest
            var dataManager = dataManagerInput;
            
            // 1) Load data (simple approach). For large datasets, convert operations to IQueryable later.
            List<TaskEntity> dataSource = await db.Tasks
                .AsNoTracking()
                .OrderBy(t => t.TaskId)
                .ToListAsync();

            IEnumerable<TaskEntity> query = dataSource;

            // 2) Searching
            if (dataManager.Search != null && dataManager.Search.Count > 0)
                query = DataOperations.PerformSearching(query, dataManager.Search);

            // 3) Filtering
            if (dataManager.Where != null && dataManager.Where.Count > 0)
                query = DataOperations.PerformFiltering(query, dataManager.Where, dataManager.Where[0].Operator);

            // 4) Sorting
            if (dataManager.Sorted != null && dataManager.Sorted.Count > 0)
                query = DataOperations.PerformSorting(query, dataManager.Sorted);

            // Total count BEFORE paging
            int count = query.Count();

            // 5) Paging
            if (dataManager.Skip != 0)
                query = DataOperations.PerformSkip(query, dataManager.Skip);

            if (dataManager.Take != 0)
                query = DataOperations.PerformTake(query, dataManager.Take);

            // Return shape required by GraphQLAdaptor: { count, result } [1](https://github.com/SyncfusionExamples/GraphQLAdaptor-Binding-Blazor-DataGrid)[2](https://blazor.syncfusion.com/documentation/data/getting-started)
            return new TaskDataResponse
            {
                Count = count,
                Result = query.ToList() // empty list is OK, never null
            };
        }
    }

    /// <summary>
    /// Response object expected by Syncfusion GraphQLAdaptor:
    /// must contain "count" and "result". [1](https://github.com/SyncfusionExamples/GraphQLAdaptor-Binding-Blazor-DataGrid)[2](https://blazor.syncfusion.com/documentation/data/getting-started)
    /// </summary>
    public class TaskDataResponse
    {
        [GraphQLName("count")]
        public int Count { get; set; }

        // IMPORTANT: must never be null (return [] for empty table)
        [GraphQLName("result")]
        public List<TaskEntity> Result { get; set; } = new();
    }
}