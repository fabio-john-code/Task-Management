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
        public async Task<IActionResult> GetById(Guid id)
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
        public async Task<IActionResult> CreateTaskItem(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return BadRequest("No description informed");
            }

            var taskItem = await _taskManagementBusiness.CreateTaskItem(description);
            return Accepted(taskItem);
        }

        [Route("{id}/status/{status}")]
        [HttpPut()]
        public async Task<IActionResult> UpdateTaskItemStatus(Guid id, TaskItem.StatusTask status)
        {
            if (Guid.Empty == id)
            {
                return BadRequest("No task id informed");
            }

            if (status == TaskItem.StatusTask.Undefined)
            {
                return BadRequest("Invalid status");
            }

            var taskItemNew = await _taskManagementBusiness.UpdateTaskItemStatus(id, status);

            return Accepted(taskItemNew);
        }
    }
}
