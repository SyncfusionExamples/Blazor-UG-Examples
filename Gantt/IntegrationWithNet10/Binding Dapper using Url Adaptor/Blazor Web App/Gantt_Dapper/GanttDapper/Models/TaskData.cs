namespace Gantt_Dapper.Models
{
    public class TaskData
    {
        public static List<TaskData> task = new List<TaskData>();

        public TaskData() { }

        public TaskData(int TaskId, string TaskName, DateTime? StartDate, DateTime? EndDate, int? ParentId, int Progress, string? Predecessor, string Duration)
        {
            this.TaskId = TaskId;
            this.TaskName = TaskName;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.ParentId = ParentId;
            this.Progress = Progress;
            this.Predecessor = Predecessor;
            this.Duration = Duration;            
        }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ParentId { get; set; }
        public int Progress { get; set; }
        public string? Predecessor { get; set; }
        public string Duration { get; set; }

        public static List<TaskData> GetAllRecords()
        {
            List<TaskData> DataCollection = new List<TaskData>();

            int x = 0;
            int duration = 0;

            // Base start date (change if needed)
            DateTime startDate = new DateTime(2026, 04, 02);

            for (int i = 1; i <= 2; i++)
            {
                // Create parent task (no separate variable name like parent1/parent2)
                int parentId = ++x;

                DataCollection.Add(new TaskData()
                {
                    TaskId = parentId,
                    TaskName = "Task " + parentId,          // Parent
                    StartDate = startDate,
                    EndDate = startDate.AddDays(26),
                    Duration = "20",
                    Progress = 0,
                    ParentId = null,
                    Predecessor = null
                });

                // Reset child start for each parent
                DateTime childStart = startDate;

                for (int j = 1; j <= 4; j++)
                {
                    // Move start like your sample: first child same date, next children based on duration+2
                    childStart = childStart.AddDays(j == 1 ? 0 : duration + 2);
                    duration = 5;

                    int childId = ++x;

                    DataCollection.Add(new TaskData()
                    {
                        TaskId = childId,
                        TaskName = "Task " + childId,        // Child
                        StartDate = childStart,
                        EndDate = childStart.AddDays(duration),
                        Duration = duration.ToString(),
                        Progress = 0,
                        ParentId = parentId,
                        Predecessor = j > 1 ? (childId - 1) + "FS" : null
                    });
                }

                // Move next parent start date after current parent ends (+2 days gap)
                startDate = startDate.AddDays(28);
                duration = 0;
            }

            return DataCollection;
        }
    }
}
