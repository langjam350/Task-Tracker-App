using TaskTrackerApp.API.NETCore.Model;

namespace TaskTrackerApp.API.NETCore.DAL
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetAllItemsAsync();

        Task<bool> AddTaskToRepositoryAsync(string title);

        Task<bool> CompleteTaskAsync(int taskId);
    }
}
