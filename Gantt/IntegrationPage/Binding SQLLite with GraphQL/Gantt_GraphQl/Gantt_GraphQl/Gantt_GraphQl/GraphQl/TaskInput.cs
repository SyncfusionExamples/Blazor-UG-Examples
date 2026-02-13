using HotChocolate;

namespace Gantt_GraphQL.GraphQl
{
    public class TaskInput
    {
        [GraphQLName("taskId")]
        public int? TaskId { get; set; }

        [GraphQLName("taskName")]
        public string? TaskName { get; set; }

        [GraphQLName("startDate")]
        public DateTime? StartDate { get; set; }

        [GraphQLName("endDate")]
        public DateTime? EndDate { get; set; }

        [GraphQLName("parentId")]
        public int? ParentId { get; set; }

        [GraphQLName("duration")]
        public int? Duration { get; set; }

        [GraphQLName("progress")]
        public int? Progress { get; set; }
    }
}