
using TaskTrackerApp.API.NETCore.DAL;
using TaskTrackerApp.API.NETCore.Model;

namespace TaskTrackerApp.API.NETCore.Service
{
    /**
     * Service for task addition, retrieval, and completion
     * Implements: ITaskService
     */
    public class TaskService : ITaskService
    {
        // Repository for tasks in memory
        private readonly ITaskRepository _tasks;

        /**
         * Constructor for TaskService
         * tasks (ITaskRepository): Task repository in-system memory for tasks
         */
        public TaskService(ITaskRepository tasks) {
            _tasks = tasks;
        }

        /**
         * Adds a task to the task repository
         * title (string): Title of task to add
         * Returns boolean detailing add status
         */
        public async Task<bool> AddTaskAsync(string title)
        {
            return await _tasks.AddTaskToRepositoryAsync(title);

        }

        /**
         * Gets all tasks from Repository
         * Returns list of TaskItems
         */
        public async Task<List<TaskItem>> GetAllTasksAsync()
        {
            return await _tasks.GetAllItemsAsync();
        }

        /**
         * Completes task
         * taskId (int): Task ID to complete
         * Returns boolean detailing completed status
         */
        public async Task<bool> CompleteTaskAsync(int taskId)
        {
            return await _tasks.CompleteTaskAsync(taskId);

        }
    }
}
