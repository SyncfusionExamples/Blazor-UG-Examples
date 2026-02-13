
using System.Net.Http.Json;
using System.Text.Json;
using static Gantt_GraphQl.Client.Pages.Counter;

namespace Gantt_GraphQl.Client.Services
{

    public class GraphQLClient
    {
        private readonly HttpClient _http;
        public GraphQLClient(HttpClient http) { _http = http; }

        public async Task<JsonElement> SendAsync(object payload)
        {
            var resp = await _http.PostAsJsonAsync("graphql", payload);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<JsonElement>()!;
        }

        public async Task<List<TaskData>> GetTasksAsync(string? search = null, int? skip = null, int? take = null, string? sortField = null, string? sortDir = null)
        {
            var q = new
            {
                query = @"query($search:String,$skip:Int,$take:Int,$sortField:String,$sortDir:String){
                  getTasks(search:$search,skip:$skip,take:$take,sortField:$sortField,sortDir:$sortDir){
                    taskId taskName startDate endDate parentId duration progress
                  }
               }",
                variables = new { search, skip, take, sortField, sortDir }
            };
            var doc = await SendAsync(q);
            var nodes = doc.GetProperty("data").GetProperty("getTasks");
            return JsonSerializer.Deserialize<List<TaskData>>(nodes.ToString(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }

        public async Task<TaskData> InsertTaskAsync(TaskData input)
        {
            var m = new
            {
                query = @"mutation($input:TaskInput){ insertTask(input:$input){ taskId taskName startDate endDate parentId duration progress } }",
                variables = new { input }
            };
            var doc = await SendAsync(m);
            var node = doc.GetProperty("data").GetProperty("insertTask");
            return JsonSerializer.Deserialize<TaskData>(node.ToString(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }

        public async Task<TaskData> UpdateTaskAsync(TaskData input)
        {
            var m = new
            {
                query = @"mutation($input:TaskInput){ updateTask(input:$input){ taskId taskName startDate endDate parentId duration progress } }",
                variables = new { input }
            };
            var doc = await SendAsync(m);
            var node = doc.GetProperty("data").GetProperty("updateTask");
            return JsonSerializer.Deserialize<TaskData>(node.ToString(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var m = new { query = "mutation($id:Int!){ deleteTask(taskId:$id) }", variables = new { id } };
            var doc = await SendAsync(m);
            return doc.GetProperty("data").GetProperty("deleteTask").GetBoolean();
        }
    }
}
