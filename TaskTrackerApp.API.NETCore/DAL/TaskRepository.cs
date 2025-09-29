using TaskTrackerApp.API.NETCore.Model;

namespace TaskTrackerApp.API.NETCore.DAL
{
    // Repository of tasks for in-memory storage and access
    // Implements: ITaskRepository interface detailing task repository methods
    public class TaskRepository : ITaskRepository
    {
        // Private variables including in-memory task store and memory lock while performing operations
        private static readonly List<TaskItem> m_tasks = new();
        public static readonly object _lock = new();

        /**
         * Method to recieve all task items from memory
         * Returns list of task items
         */
        public async Task<List<TaskItem>> GetAllItemsAsync() {
            return await Task.FromResult(m_tasks);
        }

        /**
         * Method to add a task to the repository
         * title (string): Title of task to add
         * Returns boolean if task added successfully
         */
        public async Task<bool> AddTaskToRepositoryAsync(string title) {
            TaskItem item = new TaskItem(title);
            await Task.Run(() => {
                lock (_lock) {
                    var id = m_tasks.Count + 1;
                    item.Id = id;
                    m_tasks.Add(item);
                }
            });

            return true;
        }

        /**
         * Method to complete the task based on the Task ID
         * taskId (int): Task ID to complete
         * Returns boolean if task completed successfully
         */
        public async Task<bool> CompleteTaskAsync(int taskId) {
            return await Task.Run(() => {
                lock (_lock) {
                    var taskToComplete = m_tasks.FirstOrDefault(t => t.Id == taskId);

                    if (taskToComplete == null) {
                        return false;
                    }

                    taskToComplete.IsCompleted = true;
                    return true;
                }
            });
        }
    }
}
