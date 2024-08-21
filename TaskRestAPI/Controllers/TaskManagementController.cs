using MassTransit;
using Microsoft.AspNetCore.Mvc;
using TaskManagementEntity.Model;
using TaskRestAPI.Business;
using TaskRestAPI.Business.Interfaces;

namespace TaskRestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskManagementController : ControllerBase
    {
        private readonly ITaskManagementBusiness _taskManagementBusiness;
        private readonly ILogger<TaskManagementController> _logger;
        private readonly IConfiguration _config;

        public TaskManagementController(ITaskManagementBusiness taskManagementBusiness, 
            ILogger<TaskManagementController> logger, 
            IConfiguration config)
        {
            _taskManagementBusiness = taskManagementBusiness;
            _logger = logger;
            _config = config;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _taskManagementBusiness.GetTaskById(id);
            if (result == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var result = await _taskManagementBusiness.GetTasks();
            if (result == null || result.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> CreateTaskItem([FromBody] TaskItem task)
        {
            if (string.IsNullOrWhiteSpace(task.Description))
            {
                return BadRequest("No description informed");
            }

            var taskItem = await _taskManagementBusiness.CreateTaskItem(task.Description);
            return Accepted(taskItem);
        }

        [HttpPatch()]
        public async Task<IActionResult> UpdateTaskItemStatus([FromBody] TaskItem task)
        {
            if (task.Id <= 0)
            {
                return BadRequest("No task id informed");
            }

            if (task.Status == TaskItem.StatusTask.Undefined)
            {
                return BadRequest("Invalid status");
            }

            var taskItemNew = await _taskManagementBusiness.UpdateTaskItemStatus(task.Id, task.Status);

            return Accepted(taskItemNew);
        }
    }
}
