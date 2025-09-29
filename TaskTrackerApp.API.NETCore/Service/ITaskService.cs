using TaskTrackerApp.API.NETCore.DAL;
using TaskTrackerApp.API.NETCore.Model;

namespace TaskTrackerApp.API.NETCore.Service
{
    public interface ITaskService
    {
        // Adds a task to the task repository
        public Task<bool> AddTaskAsync(string title);

        // Get all tasks from Repository
        public Task<List<TaskItem>> GetAllTasksAsync();

        // Completes task based on ID
        public Task<bool> CompleteTaskAsync(int taskId);
    }
}
