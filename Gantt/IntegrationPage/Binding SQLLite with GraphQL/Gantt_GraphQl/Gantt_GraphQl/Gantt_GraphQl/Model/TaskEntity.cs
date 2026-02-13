using System.ComponentModel.DataAnnotations;
using HotChocolate;
namespace Gantt_GraphQl.Model
{
    public class TaskEntity
    {
        [Key]
        [GraphQLName("taskId")]
        public int TaskId { get; set; }

        [GraphQLName("taskName")]
        public string TaskName { get; set; } = "New Task";

        [GraphQLName("startDate")]
        public DateTime? StartDate { get; set; }

        [GraphQLName("endDate")]
        public DateTime? EndDate { get; set; }

        [GraphQLName("parentId")]
        public int? ParentId { get; set; }

        [GraphQLName("duration")]
        public int? Duration { get; set; } = 1;

        [GraphQLName("progress")]
        public int Progress { get; set; } = 0;
    }
}

