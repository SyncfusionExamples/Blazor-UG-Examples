namespace GanttMySql.Data
{
    public class TaskDataModel
    {
        public int TaskId { get; set; }
        public string? TaskName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ParentId { get; set; }
        public string Predecessor { get; set; }
        public string? Duration { get; set; }    
        public int Progress { get; set; }
    }
}
