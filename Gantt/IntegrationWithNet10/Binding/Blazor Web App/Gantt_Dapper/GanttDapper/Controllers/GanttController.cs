using Microsoft.AspNetCore.Mvc;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace GanttDapper.Controllers
{
    /// <summary>
    /// API controller providing CRUD and query endpoints for Gantt tasks using Dapper.
    /// Accepts Syncfusion DataManager requests and returns task results suitable for UrlAdaptor.
    /// </summary>
    [ApiController]
    public class GanttController : ControllerBase
    {
        private readonly string ConnectionString = "Data Source=SYNCLAPN-46292\\SQLEXPRESS;Initial Catalog=ganttdb;Integrated Security=True;TrustServerCertificate=True;";

        /// <summary>
        /// Handles a DataManagerRequest from the client and returns filtered, searched, sorted and paged task data.
        /// </summary>
        /// <param name="DataManagerRequest">The Syncfusion <see cref="DataManagerRequest"/> containing search/filter/sort/paging instructions.</param>
        /// <returns>
        /// An object containing `result` (the task IEnumerable) and `count` (the total record count).
        /// Returned shape matches UrlAdaptor expectations.
        /// </returns>
        [HttpPost]
        [Route("api/[controller]")]       
        public object Post([FromBody] DataManagerRequest DataManagerRequest)
        {
            IEnumerable<TaskData> DataSource = GetTaskData();

            if (DataManagerRequest.Search != null && DataManagerRequest.Search.Count > 0)
                DataSource = DataOperations.PerformSearching(DataSource, DataManagerRequest.Search);

            if (DataManagerRequest.Where != null && DataManagerRequest.Where.Count > 0)
                DataSource = DataOperations.PerformFiltering(DataSource, DataManagerRequest.Where, DataManagerRequest.Where[0].Operator);

            if (DataManagerRequest.Sorted != null && DataManagerRequest.Sorted.Count > 0)
                DataSource = DataOperations.PerformSorting(DataSource, DataManagerRequest.Sorted);

            int TotalRecordsCount = DataSource.Count();

            if (DataManagerRequest.Skip != 0)
                DataSource = DataOperations.PerformSkip(DataSource, DataManagerRequest.Skip);

            if (DataManagerRequest.Take != 0)
                DataSource = DataOperations.PerformTake(DataSource, DataManagerRequest.Take);

            return new { result = DataSource, count = TotalRecordsCount };
        }

        /// <summary>
        /// Retrieves all tasks from the database ordered for hierarchical display (parents before children).
        /// </summary>
        /// <returns>A <see cref="List{TaskData}"/> containing all tasks.</returns>
        [Route("api/[controller]/GetTaskData")]
        public List<TaskData> GetTaskData()
        {
            string Query = @"SELECT TaskId, TaskName, StartDate, EndDate, ParentId, Duration, Predecessor, Progress FROM task_data
                            ORDER BY CASE WHEN ParentId IS NULL THEN 0 ELSE 1 END, ParentId, TaskId;";

            using IDbConnection Connection = new SqlConnection(ConnectionString);
            Connection.Open();
            return Connection.Query<TaskData>(Query).ToList();
        }

        /// <summary>
        /// Inserts a new task into the database and returns the created task (including assigned TaskId).
        /// </summary>
        /// <param name="Value">A <see cref="CRUDModel{TaskData}"/> whose <c>Value</c> contains the task to insert.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> with the created <see cref="TaskData"/> on success, or a BadRequest result with error details.
        /// </returns>
        [HttpPost]
        [Route("api/Gantt/Insert")]
        public IActionResult Insert([FromBody] CRUDModel<TaskData> Value)
        {
            try
            {
                var task = Value?.Value ?? new TaskData();

                // Defaults to satisfy NOT NULL columns
                task.TaskName ??= "New Task";
                task.Duration ??= 1;
                task.Progress ??= 0;
                task.StartDate ??= DateTime.Now;

                string Query = @"INSERT INTO task_data (TaskName, StartDate, EndDate, ParentId, Duration, Predecessor, Progress)
                                VALUES (@TaskName, @StartDate, @EndDate, @ParentId, @Duration,  @Predecessor, @Progress);
                                SELECT CAST(SCOPE_IDENTITY() as int);";

                using IDbConnection Connection = new SqlConnection(ConnectionString);
                Connection.Open();

                int newId = Connection.ExecuteScalar<int>(Query, task);
                task.TaskId = newId;

                // UrlAdaptor expects JSON response
                return Ok(task);
            }
            catch (Exception ex)
            {
                // return JSON (so client won't crash JSON parsing)
                return BadRequest(new { message = ex.Message, detail = ex.ToString() });
            }
        }

        /// <summary>
        /// Updates an existing task in the database.
        /// </summary>
        /// <param name="Value">A <see cref="CRUDModel{TaskData}"/> whose <c>Value</c> contains the updated task data (must include TaskId).</param>
        /// <returns>
        /// An <see cref="IActionResult"/> with the updated <see cref="TaskData"/> on success, or a BadRequest result with error details.
        /// </returns>
        [HttpPost]
        [Route("api/Gantt/Update")]
        public IActionResult Update([FromBody] CRUDModel<TaskData> Value)
        {
            try
            {
                var task = Value?.Value;

                string Query = @"
                    UPDATE task_data 
                    SET 
                        TaskName = @TaskName,
                        StartDate = @StartDate,
                        EndDate = @EndDate,
                        ParentId = @ParentId,
                        Duration = @Duration,
                        Predecessor = @Predecessor,
                        Progress = @Progress
                    WHERE TaskId = @TaskId;";

                using IDbConnection Connection = new SqlConnection(ConnectionString);
                Connection.Open();
                Connection.Execute(Query, task);

                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, detail = ex.ToString() });
            }
        }

        /// <summary>
        /// Deletes a task identified by the CRUDModel key.
        /// </summary>
        /// <param name="Value">A <see cref="CRUDModel{TaskData}"/> where <c>Key</c> identifies the TaskId to remove.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> with the deleted TaskId on success, or a BadRequest result with error details.
        /// </returns>
        [HttpPost]
        [Route("api/Gantt/Delete")]
        public IActionResult Delete([FromBody] CRUDModel<TaskData> Value)
        {
            try
            {
                int taskId = Convert.ToInt32(Value.Key?.ToString());

                string Query = "DELETE FROM task_data WHERE TaskId = @TaskId;";

                using IDbConnection Connection = new SqlConnection(ConnectionString);
                Connection.Open();
                Connection.Execute(Query, new { TaskId = taskId });

                return Ok(new { TaskId = taskId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, detail = ex.ToString() });
            }
        }

        /// <summary>
        /// Represents a task record returned to the client and stored in the `task_data` table.
        /// </summary>
        public class TaskData
        {
            [Key]
            public int? TaskId { get; set; }
            public string? TaskName { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public int? ParentId { get; set; }
            public int? Duration { get; set; }
            public string Predecessor { get; set; }
            public int? Progress { get; set; }
        }

        /// <summary>
        /// Generic CRUD wrapper used by the UrlAdaptor and server endpoints for batch and single operations.
        /// </summary>
        /// <typeparam name="T">The payload type (e.g., <see cref="TaskData"/>).</typeparam>
        public class CRUDModel<T> where T : class
        {
            /// <summary>The action type (for example: "insert", "update", "remove", "batch").</summary>
            [JsonPropertyName("action")]
            public string? Action { get; set; }

            /// <summary>The name of the key column used by the client.</summary>
            [JsonPropertyName("keyColumn")]
            public string? KeyColumn { get; set; }

            /// <summary>The key value identifying a single record (for single operations).</summary>
            [JsonPropertyName("key")]
            public object? Key { get; set; }

            /// <summary>The single entity payload for insert/update operations.</summary>
            [JsonPropertyName("value")]
            public T? Value { get; set; }

            /// <summary>Collection of added records for batch operations.</summary>
            [JsonPropertyName("added")]
            public List<T>? Added { get; set; }

            /// <summary>Collection of changed records for batch operations.</summary>
            [JsonPropertyName("changed")]
            public List<T>? Changed { get; set; }

            /// <summary>Collection of deleted records for batch operations.</summary>
            [JsonPropertyName("deleted")]
            public List<T>? Deleted { get; set; }

            /// <summary>Additional parameters sent by the client.</summary>
            [JsonPropertyName("params")]
            public IDictionary<string, object>? Params { get; set; }
        }
    }
}