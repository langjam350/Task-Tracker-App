using TaskTrackerApp.API.NETCore.DAL;
using TaskTrackerApp.API.NETCore.Model;

namespace TaskTrackerApp.API.NETCore.Service
{
    public interface ITaskService
    {
        // Adds a task to the task repository
        public bool AddTask(string title);

        // Get all tasks from Repository
        public List<TaskItem> GetAllTasks();

        // Completes task based on ID
        public bool CompleteTask(int taskId);
    }
}
