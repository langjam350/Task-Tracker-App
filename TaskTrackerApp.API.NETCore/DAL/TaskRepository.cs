using TaskTrackerApp.API.NETCore.Model;

namespace TaskTrackerApp.API.NETCore.DAL
{
    public class TaskRepository : ITaskRepository
    {
        private static readonly List<TaskItem> m_tasks = new();
        public static readonly object _lock = new();

        public List<TaskItem> GetAllItems() {
            return m_tasks;
        }

        public bool AddTaskToRepository(string title) {
            TaskItem item = new TaskItem(title);
            lock (_lock) {
                m_tasks.Add(item);
            }

            return true;
        }


        public bool CompleteTask(int taskId) {
            lock (_lock) {
                m_tasks[taskId].IsCompleted = true;
            }

            return true;
        }

        
    }
}
