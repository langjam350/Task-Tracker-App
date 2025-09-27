using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace IntegrationTests
{
    public class TaskApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public TaskApiIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task AddTask_GetAllTasks_CompleteTask()
        {
            // Arrange
            var newTask = new
            {
                Title = "This is a test task"
            };

            // Act - Add a new task
            var addTaskResponse = await _client.PostAsJsonAsync("/api/task", newTask);
            addTaskResponse.EnsureSuccessStatusCode();

            // Act - Get all tasks
            var getAllTasksResponse = await _client.GetAsync("/api/task");
            getAllTasksResponse.EnsureSuccessStatusCode();

            var tasks = await getAllTasksResponse.Content.ReadFromJsonAsync<TaskItem[]>();
            Assert.NotNull(tasks);
            Assert.NotEmpty(tasks);

            var addedTask = tasks[0];
            Assert.Equal(newTask.Title, addedTask.Title);
            Assert.False(addedTask.IsCompleted);

            // Act - Complete the task
            var completeTaskResponse = await _client.PutAsync($"/api/task/{addedTask.Id}/complete", null);
            completeTaskResponse.EnsureSuccessStatusCode();

            // Assert - Verify the task is completed
            var getCompletedTaskResponse = await _client.GetAsync($"/api/task/");
            getCompletedTaskResponse.EnsureSuccessStatusCode();

            var taskResponse = await getCompletedTaskResponse.Content.ReadAsStringAsync();
            Assert.NotNull(taskResponse);

            // Deserialize the JSON string to TaskItem[]
            var tasksAfterComplete = JsonSerializer.Deserialize<TaskItem[]>(taskResponse);

            // Check the first task's IsCompleted status
            Assert.NotNull(tasks);
            Assert.True(tasksAfterComplete[0].IsCompleted);

        }
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [JsonPropertyName("isCompleted")]
        public bool IsCompleted { get; set; }
    }
}
