namespace TreeGridUGSample.Components.Data
{
    public class BusinessObject
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime? StartDate { get; set; }
        public int Duration { get; set; }
        public int Progress { get; set; }
        public string Priority { get; set; }
        public int? ParentId { get; set; }
        public Boolean Approved { get; set; }
    
     public static List<BusinessObject> GetSelfDataSource()
        {
            List<BusinessObject> BusinessObjectCollection = new List<BusinessObject>();
            BusinessObjectCollection.Add(new BusinessObject()
            {
                TaskId = 1,
                TaskName = "Parent Task 1",
                StartDate = new DateTime(2017, 10, 23),
                Duration = 10,
                Progress = 70,
                Priority = "Critical",
                ParentId = null
            });
            BusinessObjectCollection.Add(new BusinessObject()
            {
                TaskId = 2,
                TaskName = "Child task 1",
                StartDate = new DateTime(2017, 10, 23),
                Duration = 12,
                Progress = 80,
                Priority = "Low",
                ParentId = 1
            });
            BusinessObjectCollection.Add(new BusinessObject()
            {
                TaskId = 3,
                TaskName = "Child Task 2",
                StartDate = new DateTime(2017, 10, 24),
                Duration = 5,
                Progress = 65,
                Priority = "Critical",
                ParentId = 2
            });
            BusinessObjectCollection.Add(new BusinessObject()
            {
                TaskId = 4,
                TaskName = "Child task 3",
                StartDate = new DateTime(2017, 10, 25),
                Duration = 6,
                Priority = "High",
                Progress = 77,
                ParentId = 3
            });
            BusinessObjectCollection.Add(new BusinessObject()
            {
                TaskId = 5,
                TaskName = "Child Task 5",
                StartDate = new DateTime(2017, 10, 26),
                Duration = 9,
                Progress = 25,
                ParentId = 4,
                Priority = "Normal"
            });
            BusinessObjectCollection.Add(new BusinessObject()
            {
                TaskId = 6,
                TaskName = "Child Task 6",
                StartDate = new DateTime(2017, 10, 27),
                Duration = 9,
                Progress = 7,
                ParentId = 5,
                Priority = "Normal"
            });
            BusinessObjectCollection.Add(new BusinessObject()
            {
                TaskId = 7,
                TaskName = "Parent Task 3",
                StartDate = new DateTime(2017, 10, 28),
                Duration = 4,
                Progress = 45,
                ParentId = null,
                Priority = "High"
            });
            BusinessObjectCollection.Add(new BusinessObject()
            {
                TaskId = 8,
                TaskName = "Child Task 7",
                StartDate = new DateTime(2017, 10, 29),
                Duration = 3,
                Progress = 38,
                ParentId = 7,
                Priority = "Critical"
            });
            BusinessObjectCollection.Add(new BusinessObject()
            {
                TaskId = 9,
                TaskName = "Child Task 8",
                StartDate = new DateTime(2017, 10, 30),
                Duration = 7,
                Progress = 70,
                ParentId = 7,
                Priority = "Low"
            });
            return BusinessObjectCollection;
        }
    }
}
