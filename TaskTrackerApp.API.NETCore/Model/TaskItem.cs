using System.Text.Json.Serialization;

namespace TaskTrackerApp.API.NETCore.Model
{
    public class TaskItem
    {
        // Define new task item given title
        public TaskItem(string title) { 
            Title = title;
        }
        // Generate new GUID for Task Item
        public int Id { get; set; }

        // Title for task defined in front-end program
        public string Title { get; set; }

        // Completion Status of task
        [JsonPropertyName("isCompleted")]
        public bool IsCompleted { get; set; }
    }
}
