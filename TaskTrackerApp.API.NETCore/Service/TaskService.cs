
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
        public bool AddTask(string title)
        {
            return _tasks.AddTaskToRepository(title);
            
        }

        /**
         * Gets all tasks from Repository
         * Returns list of TaskItems
         */
        public List<TaskItem> GetAllTasks()
        {
            return _tasks.GetAllItems();
        }

        /**
         * Completes task
         * taskId (int): Task ID to complete
         * Returns boolean detailing completed status
         */
        public bool CompleteTask(int taskId)
        {
            return _tasks.CompleteTask(taskId);
            
        }
    }
}
