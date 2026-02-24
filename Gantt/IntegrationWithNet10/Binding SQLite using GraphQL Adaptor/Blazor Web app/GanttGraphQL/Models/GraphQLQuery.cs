using HotChocolate;                        
using HotChocolate.Types;                  
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;
namespace GanttGraphQL.Models
{
    /// <summary>
    /// GraphQL query resolver for the Syncfusion GraphQLAdaptor used by the Gantt Chart.
    /// Exposes a field named <c>taskData</c> that accepts DataManager request parameters
    /// and returns a payload containing the result set and total record count.
    /// </summary>
    public class GraphQLQuery
    {
        /// <summary>
        /// Retrieves Gantt Chart task data and returns it along with the total record count.
        /// Applies searching, filtering, sorting, skip, and take based on the incoming DataManager request parameters from the GraphQLAdaptor.
        /// </summary>
        /// <param name="dataManager">
        /// The data manager request input containing query parameters such as search, where (filters), sorted (multi-column sort), skip, take</c>.
        /// </param>
        /// <param name="db">The EF Core database context used to query task records.</param>
        /// <returns>
        /// An instance of <see cref="TaskDataResponse"/> containing the filtered task list
        /// and the corresponding record count (total count when <c>RequiresCounts</c> is true).
        /// </returns>
        [GraphQLName("taskData")]
        public async Task<TaskDataResponse> GetTaskData([GraphQLName("dataManager")] DataManagerRequestInput dataManagerInput, [Service] GanttDbContext db)
        {
            DataManagerRequestInput dm = dataManagerInput ?? new DataManagerRequestInput
            {
                Skip = 0,
                Take = 0,
                RequiresCounts = false
            };
            
            List<TaskDataModel> dataSource = await db.Tasks
                                     .AsNoTracking()
                                     .OrderBy(t => t.TaskID)
                                     .ToListAsync();

            IEnumerable<TaskDataModel> query = dataSource;

            // 1) Searching
            if (dm.Search is { Count: > 0 })
            {
                query = DataOperations.PerformSearching(query, dm.Search);
            }

            // 2) Filtering
            if (dm.Where is { Count: > 0 })
            {
                // Base condition "and"/"or" (default to "and" if missing)
                string? baseCondition = dm.Where[0].Condition ?? dm.Where[0].Operator;
                if (string.IsNullOrWhiteSpace(baseCondition)) baseCondition = "and";

                query = DataOperations.PerformFiltering(query, dm.Where, baseCondition);
            }

            // 3) Sorting (multi-column supported)
            if (dm.Sorted is { Count: > 0 })
            {
                query = DataOperations.PerformSorting(query, dm.Sorted);
            }

            int totalCount = query.Count();
            
            if (dm.Skip > 0) query = DataOperations.PerformSkip(query, dm.Skip);
            if (dm.Take > 0) query = DataOperations.PerformTake(query, dm.Take);

            return new TaskDataResponse
            {
                Count = dm.RequiresCounts ? totalCount : query.Count(), // return total only if asked
                Result = query.ToList()
            };
        }
    }

    /// <summary>
    /// Response payload expected by the Syncfusion GraphQLAdaptor for list queries.
    /// Must expose <c>count</c> and <c>result</c> fields to enable paging and total count.
    /// </summary>
    public class TaskDataResponse
    {
        /// <summary>
        /// Total number of records for the current query.
        /// When <c>RequiresCounts</c> is true in the request, this should reflect the total
        /// count before paging; otherwise it may be the count of the returned page.
        /// </summary>
        [GraphQLName("count")]
        public int Count { get; set; }

        /// <summary>
        /// The collection of task records returned for the current request.
        /// </summary>
        [GraphQLName("result")]
        public List<TaskDataModel> Result { get; set; } = new List<TaskDataModel>();
    }
}