using Microsoft.AspNetCore.Mvc;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Gantt_Dapper.Controllers
{
    [ApiController]
    public class GanttController : ControllerBase
    {
        private readonly string ConnectionString = "Data Source=SYNCLAPN-46292\\SQLEXPRESS;Initial Catalog=ganttdb;Integrated Security=True;TrustServerCertificate=True;";

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

        [Route("api/[controller]/GetTaskData")]
        public List<TaskData> GetTaskData()
        {
            string Query = @"
        SELECT 
            TaskId,
            TaskName,
            StartDate,
            EndDate,
            ParentId,
            Duration,
            Progress
        FROM task_data
        ORDER BY TaskId;";

            using IDbConnection Connection = new SqlConnection(ConnectionString);
            Connection.Open();
            return Connection.Query<TaskData>(Query).ToList();
        }

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

                string Query = @"INSERT INTO task_data (TaskName, StartDate, EndDate, ParentId, Duration, Progress)
                                VALUES (@TaskName, @StartDate, @EndDate, @ParentId, @Duration, @Progress);
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

        public class TaskData
        {
            [Key]
            public int? TaskId { get; set; }
            public string? TaskName { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public int? ParentId { get; set; }
            public int? Duration { get; set; }
            public int Progress { get; set; }
        }

        // Use System.Text.Json attributes (works by default in modern ASP.NET Core)
        public class CRUDModel<T> where T : class
        {
            [JsonPropertyName("action")]
            public string? Action { get; set; }

            [JsonPropertyName("keyColumn")]
            public string? KeyColumn { get; set; }

            [JsonPropertyName("key")]
            public object? Key { get; set; }

            [JsonPropertyName("value")]
            public T? Value { get; set; }

            [JsonPropertyName("added")]
            public List<T>? Added { get; set; }

            [JsonPropertyName("changed")]
            public List<T>? Changed { get; set; }

            [JsonPropertyName("deleted")]
            public List<T>? Deleted { get; set; }

            [JsonPropertyName("params")]
            public IDictionary<string, object>? Params { get; set; }
        }
    }
}