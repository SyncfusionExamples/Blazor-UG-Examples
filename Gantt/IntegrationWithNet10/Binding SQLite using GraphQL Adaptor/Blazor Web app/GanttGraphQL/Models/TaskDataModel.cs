using System.Text.Json.Serialization;

namespace GanttGraphQL.Models
{   
    public class TaskDataModel
    {       
        [JsonPropertyName("taskID")]
        public int? TaskID { get; set; }

        [JsonPropertyName("taskName")]
        public string? TaskName { get; set; }

        [JsonPropertyName("startDate")]
        public DateTime? StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime? EndDate { get; set; }

        [JsonPropertyName("duration")]
        public string? Duration { get; set; }

        [JsonPropertyName("progress")]
        public int? Progress { get; set; }

        [JsonPropertyName("predecessor")]
        public string? Predecessor { get; set; }

        [JsonPropertyName("parentID")]
        public int? ParentID { get; set; }
    }
}


