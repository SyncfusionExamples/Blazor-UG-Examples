using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace  KanbanUGSample.Components.Pages.Kanban.ExternalDragAndDrop.Data
{
    public class HospitalData
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Nullable<bool> IsAllDay { get; set; }
        public string CategoryColor { get; set; }
        public string RecurrenceRule { get; set; }
        public Nullable<int> RecurrenceID { get; set; }
        public Nullable<int> FollowingID { get; set; }
        public string RecurrenceException { get; set; }
        public string StartTimezone { get; set; }
        public string EndTimezone { get; set; }
        public string Name { get; set; }
        public int DepartmentID { get; set; }
        public int ConsultantID { get; set; }
        public string DepartmentName { get; set; }
    }

    public class ScheduleData
    {

        public List<HospitalData> GetHospitalData()
        {
            List<HospitalData> hospitalData = new List<HospitalData>();
            hospitalData.Add(new HospitalData
            {
                Id = 10,
                Name = "David",
                StartTime = new DateTime(2020, 1, 6, 9, 0, 0),
                EndTime = new DateTime(2020, 1, 6, 10, 0, 0),
                Description = "Health Checkup",
                DepartmentID = 1,
                ConsultantID = 1,
                DepartmentName = "GENERAL"
            });
            hospitalData.Add(new HospitalData
            {
                Id = 11,
                Name = "John",
                StartTime = new DateTime(2020, 1, 6, 10, 30, 0),
                EndTime = new DateTime(2020, 1, 6, 11, 30, 0),
                Description = "Tooth Erosion",
                DepartmentID = 2,
                ConsultantID = 2,
                DepartmentName = "DENTAL"
            });
            hospitalData.Add(new HospitalData
            {
                Id = 12,
                Name = "Peter",
                StartTime = new DateTime(2020, 1, 6, 12, 0, 0),
                EndTime = new DateTime(2020, 1, 6, 13, 0, 0),
                Description = "Eye and Spectacles Checkup",
                DepartmentID = 1,
                ConsultantID = 3,
                DepartmentName = "GENERAL"
            });
            hospitalData.Add(new HospitalData
            {
                Id = 13,
                Name = "Starc",
                StartTime = new DateTime(2020, 1, 6, 14, 0, 0),
                EndTime = new DateTime(2020, 1, 6, 15, 0, 0),
                Description = "Toothaches",
                DepartmentID = 2,
                ConsultantID = 4,
                DepartmentName = "DENTAL"
            });
            hospitalData.Add(new HospitalData
            {
                Id = 14,
                Name = "James",
                StartTime = new DateTime(2020, 1, 6, 10, 0, 0),
                EndTime = new DateTime(2020, 1, 6, 11, 0, 0),
                Description = "Surgery Appointment",
                DepartmentID = 1,
                ConsultantID = 5,
                DepartmentName = "GENERAL"
            });
            hospitalData.Add(new HospitalData
            {
                Id = 15,
                Name = "Jercy",
                StartTime = new DateTime(2020, 1, 6, 9, 30, 0),
                EndTime = new DateTime(2020, 1, 6, 10, 30, 0),
                Description = "Tooth Sensitivity",
                DepartmentID = 2,
                ConsultantID = 6,
                DepartmentName = "DENTAL"
            });
            hospitalData.Add(new HospitalData
            {
                Id = 16,
                Name = "Albert",
                StartTime = new DateTime(2020, 1, 7, 10, 0, 0),
                EndTime = new DateTime(2020, 1, 7, 11, 30, 0),
                Description = "Skin care treatment",
                DepartmentID = 1,
                ConsultantID = 7,
                DepartmentName = "GENERAL"
            });
            hospitalData.Add(new HospitalData
            {
                Id = 17,
                Name = "Louis",
                StartTime = new DateTime(2020, 1, 7, 12, 30, 0),
                EndTime = new DateTime(2020, 1, 7, 13, 30, 0),
                Description = "General Checkup",
                DepartmentID = 1,
                ConsultantID = 9,
                DepartmentName = "DENTAL"
            });
            hospitalData.Add(new HospitalData
            {
                Id = 18,
                Name = "Williams",
                StartTime = new DateTime(2020, 1, 7, 12, 0, 0),
                EndTime = new DateTime(2020, 1, 7, 14, 0, 0),
                Description = "Mouth Sores",
                DepartmentID = 2,
                ConsultantID = 10,
                DepartmentName = "DENTAL"
            });
            hospitalData.Add(new HospitalData
            {
                Id = 19,
                Name = "David",
                StartTime = new DateTime(2020, 1, 7, 16, 30, 0),
                EndTime = new DateTime(2020, 1, 7, 18, 45, 0),
                Description = "Eye checkup and Treatment",
                DepartmentID = 1,
                ConsultantID = 1,
                DepartmentName = "GENERAL"
            });
            hospitalData.Add(new HospitalData
            {
                Id = 20,
                Name = "John",
                StartTime = new DateTime(2020, 1, 7, 19, 30, 0),
                EndTime = new DateTime(2020, 1, 7, 21, 45, 0),
                Description = "Tooth Decay",
                DepartmentID = 2,
                ConsultantID = 2,
                DepartmentName = "DENTAL"
            });
            return hospitalData;
        }
        public List<HospitalData> GetWaitingListData()
        {
            List<HospitalData> waitingData = new List<HospitalData>();
            waitingData.Add(new HospitalData
            {
                Id = 1,
                Name = "Steven",
                StartTime = new DateTime(2020, 1, 3, 7, 30, 0),
                EndTime = new DateTime(2020, 1, 3, 9, 30, 0),
                Description = "Consulting",
                DepartmentName = "GENERAL"
            });
            waitingData.Add(new HospitalData
            {
                Id = 2,
                Name = "Milan",
                StartTime = new DateTime(2020, 1, 4, 8, 30, 0),
                EndTime = new DateTime(2020, 1, 4, 10, 30, 0),
                Description = "Bad Breath",
                DepartmentName = "DENTAL"
            });
            waitingData.Add(new HospitalData
            {
                Id = 3,
                Name = "Laura",
                StartTime = new DateTime(2020, 1, 4, 9, 30, 0),
                EndTime = new DateTime(2020, 1, 4, 10, 30, 0),
                Description = "Extraction",
                DepartmentName = "GENERAL"
            });
            waitingData.Add(new HospitalData
            {
                Id = 4,
                Name = "Janet",
                StartTime = new DateTime(2020, 1, 3, 11, 0, 0),
                EndTime = new DateTime(2020, 1, 3, 12, 30, 0),
                Description = "Gum Disease",
                DepartmentName = "DENTAL"
            });
            waitingData.Add(new HospitalData
            {
                Id = 5,
                Name = "Adams",
                StartTime = new DateTime(2020, 1, 3, 11, 0, 0),
                EndTime = new DateTime(2020, 1, 3, 12, 30, 0),
                Description = "Observation",
                DepartmentName = "GENERAL"
            });
            waitingData.Add(new HospitalData
            {
                Id = 6,
                Name = "John",
                StartTime = new DateTime(2020, 1, 3, 11, 30, 0),
                EndTime = new DateTime(2020, 1, 3, 12, 30, 0),
                Description = "Mouth Sores",
                DepartmentName = "DENTAL"
            });
            return waitingData;
        }

    }
    public class KanbanDataModel
    {
        public string Id { get; set; }
        public int ListId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string Summary { get; set; }
        public string Type { get; set; }
        public string Priority { get; set; }
        public List<string> CardTags { get; set; }
        public string Tags { get; set; }
        public double Estimate { get; set; }
        public string Assignee { get; set; }
        public int RankId { get; set; }
        public string Color { get; set; }
        public string Value { get; set; }
        public string OrderID { get; set; }
        public string Size { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Price { get; set; }
        public string AssigneeKey { get; set; }
        public List<string> ClassName { get; set; }

        public List<KanbanDataModel> GetTasks()
        {
            List<KanbanDataModel> TaskDetails = new List<KanbanDataModel>
            {
                new KanbanDataModel { Id = "11", Title = "Task  - 29016", Status = "In Progress", Summary = "Fix cannot open user’s default database SQL error.", Priority = "Critical", Type = "High", CardTags = new List<string>() { "Database", "Sql2008" }, Estimate = 2.5, Assignee = "Janet Leverling", AssigneeKey = "Janet Leverling", RankId = 4, Color = "#cc0000", ClassName = new List<string>() { "e-critical", "e-high", "e-janet" } },
                new KanbanDataModel { Id = "12", Title = "Task  - 29017", Status = "Review", Summary = "Fix the issues reported in data binding.", Type = "Story", Priority = "Normal", CardTags = new List<string>() { "Databinding" }, Estimate = 3.5, Assignee = "Janet Leverling", AssigneeKey = "Janet Leverling", RankId = 4, Color = "#8b447a", ClassName = new List<string>() { "e-story", "e-noraml", "e-janet" } },
                new KanbanDataModel { Id = "13", Title = "Task  - 29018", Status = "Close", Summary = "Analyze SQL server 2008 connection.", Type = "Story", Priority = "Release Breaker", CardTags = new List<string>() { "Grid", "Sql" }, Estimate = 2, Assignee = "Andrew Fuller", AssigneeKey = "Andrew Fuller", RankId = 4, Color = "#8b447a", ClassName = new List<string>() { "e-story", "e-release", "e-andrew" } },
                new KanbanDataModel { Id = "14", Title = "Task  - 29019", Status = "Validate", Summary = "Validate databinding issues.", Type = "Story", Priority = "Low", CardTags = new List<string>() { "Validation" }, Estimate = 1.5, Assignee = "Margaret hamilt", AssigneeKey = "Margaret hamilt", RankId = 1, Color = "#8b447a", ClassName = new List<string>() { "e-story", "e-low", "e-margaret" } },
                new KanbanDataModel { Id = "15", Title = "Task  - 29020", Status = "Close", Summary = "Analyze grid control.", Type = "Story", Priority = "High", CardTags = new List<string>() { "Analyze" }, Estimate = 2.5, Assignee = "Margaret hamilt", AssigneeKey = "Margaret hamilt", RankId = 5, Color = "#8b447a", ClassName = new List<string>() { "e-story", "e-high", "e-margaret" } },
                new KanbanDataModel { Id = "16", Title = "Task  - 29021", Status = "Close", Summary = "Stored procedure for initial data binding of the grid.", Type = "Others", Priority = "Release Breaker", CardTags = new List<string>() { "Databinding" }, Estimate = 1.5, Assignee = "Steven walker", AssigneeKey = "Steven walker", RankId = 6, Color = "#27AE60", ClassName = new List<string>() { "e-others", "e-release", "e-steven" } },
                new KanbanDataModel { Id = "17", Title = "Task  - 29022", Status = "Close", Summary = "Analyze stored procedures.", Type = "Story", Priority = "Release Breaker", CardTags = new List<string>() { "Procedures" }, Estimate = 5.5, Assignee = "Janet Leverling", AssigneeKey = "Janet Leverling", RankId = 7, Color = "#8b447a", ClassName = new List<string>() { "e-story", "e-release", "e-janet" } },
                new KanbanDataModel { Id = "18", Title = "Task  - 29023", Status = "Validate", Summary = "Validate editing issues.", Type = "Story", Priority = "Critical", CardTags = new List<string>() { "Editing" }, Estimate = 1, Assignee = "Nancy Davloio", AssigneeKey = "Nancy Davloio", RankId = 1, Color = "#8b447a", ClassName = new List<string>() { "e-story", "e-critical", "e-nancy" } },
                new KanbanDataModel { Id = "19", Title = "Task  - 29024", Status = "Review", Summary = "Test editing functionality.", Type = "Story", Priority = "Normal", CardTags = new List<string>() { "Editing", "Test" }, Estimate = 0.5, Assignee = "Nancy Davloio", AssigneeKey = "Nancy Davloio", RankId = 5, Color = "#8b447a", ClassName = new List<string>() { "e-story", "e-normal", "e-nancy" } },
                new KanbanDataModel { Id = "20", Title = "Task  - 29025", Status = "Open", Summary = "Enhance editing functionality.", Type = "Improvement", Priority = "Low", CardTags = new List<string>() { "Editing" }, Estimate = 3.5, Assignee = "Andrew Fuller", AssigneeKey = "Andrew Fuller", RankId = 5, Color = "#7d7297", ClassName = new List<string>() { "e-imrpovement", "e-low", "e-andrew" } },
            };
            return TaskDetails;
        }

        public List<KanbanDataModel> GetCardTasks()
        {
            List<KanbanDataModel> CardData = new List<KanbanDataModel>
            {
                new KanbanDataModel { Id = "Task 1", Title = "Task  - 29001", Status = "Open", Summary = "Analyze customer requirements.", Priority = "High", CardTags = new List<string>() { "Bug", "Release Bug" }, RankId = 1, Assignee = "Nancy Davloio" },
                new KanbanDataModel { Id = "Task 2", Title = "Task  - 29002", Status = "In Progress", Summary = "Add responsive support to applicaton", Priority = "Low", CardTags = new List<string>() { "Story", "Kanban" }, RankId = 1, Assignee = "Andrew Fuller" },
                new KanbanDataModel { Id = "Task 3", Title = "Task  - 29003", Status = "Open", Summary = "Show the retrived data from the server in grid control.", Priority = "High", CardTags = new List<string>() { "Bug", "Breaking Issue" }, RankId = 2, Assignee = "Janet Leverling" },
                new KanbanDataModel { Id = "Task 4", Title = "Task  - 29004", Status = "Open", Summary = "Fix the issues reported in the IE browser.", Priority = "High", CardTags = new List<string>() { "Bug", "Customer" }, RankId = 3, Assignee = "Andrew Fuller" },
                new KanbanDataModel { Id = "Task 5", Title = "Task  - 29005", Status = "Review", Summary = "Improve application performance.", Priority = "Normal", CardTags = new List<string>() { "Story", "Kanban" }, RankId = 1, Assignee = "Steven walker" },
                new KanbanDataModel { Id = "Task 6", Title = "Task  - 29009", Status = "Review", Summary = "API Improvements.", Priority = "Critical", CardTags = new List<string>() { "Bug", "Customer" }, RankId = 2, Assignee = "Nancy Davloio" },
                new KanbanDataModel { Id = "Task 7", Title = "Task  - 29010", Status = "Close", Summary = "Fix cannot open users default database sql error.", Priority = "High", CardTags = new List<string>() { "Bug", "Internal" }, RankId = 8, Assignee = "Margaret hamilt" },
                new KanbanDataModel { Id = "Task 8", Title = "Task  - 29015", Status = "Open", Summary = "Fix the filtering issues reported in safari.", Priority = "Critical", CardTags = new List<string>() { "Bug", "Breaking Issue" }, RankId = 4, Assignee = "Margaret hamilt" },
                new KanbanDataModel { Id = "Task 9", Title = "Task  - 29016", Status = "Review", Summary = "Fix the issues reported in IE browser.", Priority = "High", CardTags = new List<string>() { "Bug", "Customer" }, RankId = 3, Assignee = "Andrew Fuller" },
                new KanbanDataModel { Id = "Task 10", Title = "Task  - 29017", Status = "Review", Summary = "Enhance editing functionality.", Priority = "Normal", CardTags = new List<string>() { "Story", "Kanban" }, RankId = 4, Assignee = "Janet Leverling" },
                new KanbanDataModel { Id = "Task 11", Title = "Task  - 29018", Status = "Close", Summary = "Arrange web meeting with customer to get login page requirements.", Priority = "High", CardTags = new List<string>() { "Feature" }, RankId = 1, Assignee = "Andrew Fuller" },
                new KanbanDataModel { Id = "Task 12", Title = "Task  - 29020", Status = "Close", Summary = "Login page validation.", Priority = "Low", CardTags = new List<string>() { "Bug" }, RankId = 2, Assignee = "Margaret hamilt" },
                new KanbanDataModel { Id = "Task 13", Title = "Task  - 29021", Status = "Close", Summary = "Test the application in IE browser.", Priority = "Normal", CardTags = new List<string>() { "Bug" }, RankId = 3, Assignee = "Steven walker" },
                new KanbanDataModel { Id = "Task 14", Title = "Task  - 29022", Status = "Close", Summary = "Analyze stored procedure.", Priority = "Critical", CardTags = new List<string>() { "CustomSample", "Customer" }, RankId = 4, Assignee = "Janet Leverling" },
                new KanbanDataModel { Id = "Task 15", Title = "Task  - 29024", Status = "Review", Summary = "Check login page validation.", Priority = "Low", CardTags = new List<string>() { "Story" }, RankId = 5, Assignee = "Nancy Davloio" },
                new KanbanDataModel { Id = "Task 16", Title = "Task  - 29025", Status = "Close", Summary = "Add input validation for editing.", Priority = "Critical", CardTags = new List<string>() { "Bug", "Breaking Issue" }, RankId = 5, Assignee = "Andrew Fuller" },
                new KanbanDataModel { Id = "Task 17", Title = "Task  - 29026", Status = "In Progress", Summary = "Improve performance of editing functionality.", Priority = "Critical", CardTags = new List<string>() { "Bug", "Customer" }, RankId = 2, Assignee = "Nancy Davloio" },
                new KanbanDataModel { Id = "Task 18", Title = "Task  - 29027", Status = "Open", Summary = "Arrange web meeting for cutomer requirements.", Priority = "High", CardTags = new List<string>() { "Story" }, RankId = 5, Assignee = "Steven walker" },
                new KanbanDataModel { Id = "Task 19", Title = "Task  - 29029", Status = "Review", Summary = "Fix the issues reported by the customer.", Priority = "High", CardTags = new List<string>() { "Bug" }, RankId = 6, Assignee = "Janet Leverling" },
                new KanbanDataModel { Id = "Task 20", Title = "Task  - 29030", Status = "In Progress", Summary = "Test editing functionality", Priority = "Low", CardTags = new List<string>() { "Story" }, RankId = 3, Assignee = "Janet Leverling" },
                new KanbanDataModel { Id = "Task 21", Title = "Task  - 29031", Status = "In Progress", Summary = "Check filtering validation", Priority = "Normal", CardTags = new List<string>() { "Feature", "Release" }, RankId = 4, Assignee = "Janet Leverling" },
                new KanbanDataModel { Id = "Task 22", Title = "Task  - 29032", Status = "In Progress", Summary = "Arrange web meeting with customer to get login page requirements", Priority = "Critical", CardTags = new List<string>() { "Feature" }, RankId = 5, Assignee = "Margaret hamilt" },
                new KanbanDataModel { Id = "Task 23", Title = "Task  - 29033", Status = "Open", Summary = "Arrange web meeting with customer to get editing requirements", Priority = "Critical", CardTags = new List<string>() { "Story", "Improvement" }, RankId = 6, Assignee = "Andrew Fuller" },
                new KanbanDataModel { Id = "Task 24", Title = "Task  - 29034", Status = "In Progress", Summary = "Fix the issues reported by the customer.", Priority = "Critical", CardTags = new List<string>() { "Bug", "Customer" }, RankId = 6, Assignee = "Steven walker" },
                new KanbanDataModel { Id = "Task 25", Title = "Task  - 29035", Status = "Close", Summary = "Fix the issues reported in Safari browser.", Priority = "High", CardTags = new List<string>() { "Bug" }, RankId = 6, Assignee = "Nancy Davloio" },
                new KanbanDataModel { Id = "Task 26", Title = "Task  - 29036", Status = "Review", Summary = "Check Login page validation.", Priority = "Critical", CardTags = new List<string>() { "Bug", "Customer" }, RankId = 7, Assignee = "Margaret hamilt" },
                new KanbanDataModel { Id = "Task 27", Title = "Task  - 29037", Status = "Open", Summary = "Fix the issues reported in data binding.", Priority = "Normal", CardTags = new List<string>() { "Bug" }, Estimate = 3.5, RankId = 7, Assignee = "Steven walker" },
                new KanbanDataModel { Id = "Task 28", Title = "Task  - 29038", Status = "Close", Summary = "Test editing functionality.", Priority = "Normal", CardTags = new List<string>() { "Story" }, Estimate = 0.5, RankId = 7, Assignee = "Steven walker" }
            };
            return CardData;
        }
    }
}