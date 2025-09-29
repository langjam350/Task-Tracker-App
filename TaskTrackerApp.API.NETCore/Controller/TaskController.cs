using Microsoft.AspNetCore.Mvc;
using TaskTrackerApp.API.NETCore.Service;

namespace TaskTrackerApp.API.NETCore.Controller
{

    public class CreateTaskRequest
    {
        public string Title { get; set; } = string.Empty;
    }

    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /**
         * GET: api/tasks
         * Gets all tasks from repository
         * Returns list of all TaskItems
         */
        [HttpGet]
        public async Task<ActionResult> GetAllTasks()
        {
            // Get All Tasks from task service
            var tasks = await _taskService.GetAllTasksAsync();

            // Return 200 with task list if present and 404 if tasks are missing
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<ActionResult> AddTask([FromBody] CreateTaskRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest(new { message = "Title is required" });
            }

            var result = await _taskService.AddTaskAsync(request.Title);
            return result ? Ok() : BadRequest();
        }

        [HttpPut("{id}/complete")]
        public async Task<ActionResult> CompleteTask([FromRoute(Name = "id")] int taskId)
        {
            // Complete task using task service implementation
            var result = await _taskService.CompleteTaskAsync(taskId);

            // Return 200 upon successful completion and 404 if the task is not found
            return result ? Ok() : NotFound();
        }
    }
}
