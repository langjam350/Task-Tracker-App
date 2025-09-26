using Microsoft.AspNetCore.Mvc;
using TaskTrackerApp.API.NETCore.Service;

namespace TaskTrackerApp.API.NETCore.Controller
{
    [ApiController]
    [Route("api/controller]")]
    public class TaskController
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
        public ActionResult<string> GetAllTasks()
        {var tasks = _taskService.GetAllTasks();
            return Ok(tasks);
        }
    }
}
