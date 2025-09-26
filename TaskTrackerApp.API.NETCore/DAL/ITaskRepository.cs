using TaskTrackerApp.API.NETCore.Model;

namespace TaskTrackerApp.API.NETCore.DAL
{
    public interface ITaskRepository
    {
        List<TaskItem> GetAllItems();

        bool AddTaskToRepository(string title);

        bool CompleteTask(int taskId);
    }
}
