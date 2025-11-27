using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TreeGrid_WasmApp.Client.Pages.Data
{
    public class TreeData
    {
        public class BusinessObject : INotifyPropertyChanged
        {
            public int TaskId { get; set; }

            private string taskName;
            public string TaskName
            {
                get => taskName;
                set
                {
                    taskName = value;
                    NotifyPropertyChanged(nameof(TaskName));
                }
            }

            public int? Duration { get; set; }
            public int? Progress { get; set; }
            public string Priority { get; set; }
            public int? ParentId { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;
            private void NotifyPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static ObservableCollection<BusinessObject> GetSelfDataSource()
        {
            return new ObservableCollection<BusinessObject>
          {
              new BusinessObject { TaskId = 1, TaskName = "Parent Task 1", Duration = 10, Progress = 70, Priority = "Critical", ParentId = null },
              new BusinessObject { TaskId = 2, TaskName = "Child task 1", Duration = 6, Progress = 80, Priority = "Low", ParentId = 1 },
              new BusinessObject { TaskId = 3, TaskName = "Child Task 2", Duration = 5, Progress = 65, Priority = "Critical", ParentId = 2 },
              new BusinessObject { TaskId = 4, TaskName = "Child task 3", Duration = 6, Priority = "High", Progress = 77, ParentId = 3 },
              new BusinessObject { TaskId = 5, TaskName = "Parent Task 2", Duration = 10, Progress = 70, Priority = "Critical", ParentId = null }
          };
        }
    }
}