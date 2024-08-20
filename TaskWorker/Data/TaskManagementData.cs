using MassTransit;
using Microsoft.EntityFrameworkCore;
using TaskManagementEntity.Model;
using TaskWorker.Data;
using TaskWorker.TaskManagement.Interfaces;

namespace TaskWorker.TaskManagement
{
    public class TaskManagementData : ITaskManagementData
    {
        private readonly ILogger<TaskManagementData> _logger;
        private readonly IBus _bus;
        private readonly TaskManagementContext _taskManagementContext;
        private static List<TaskItem> taskItems = [];

        public TaskManagementData(TaskManagementContext taskManagementContext,
            ILogger<TaskManagementData> logger,
            IBus bus)
        {
            _logger = logger;
            _bus = bus;
            _taskManagementContext = taskManagementContext;
        }

        public async Task CreateTaskItem(TaskItem taskItem)
        {
            try
            {
                await _taskManagementContext.Tasks.AddAsync(taskItem);
                await _taskManagementContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Task Creation failed");
                throw;
            }
        }

        public async Task<TaskItem> GetTaskById(int id)
        {
            var result = await _taskManagementContext.Tasks.FindAsync(id);

            return result;
        }

        public async Task<List<TaskItem>> GetTasks()
        {
            var result = await _taskManagementContext.Tasks.ToListAsync();

            return result;
        }

        public async Task UpdateTaskItem(TaskItem taskItemNew)
        {
            var taskItem = await _taskManagementContext.Tasks.FindAsync(taskItemNew.Id); 
            taskItem.Description = taskItemNew.Description;
            taskItem.Status = taskItemNew.Status;
            taskItem.DateUpdated = DateTime.Now;
            _taskManagementContext.Tasks.Update(taskItem);
            await _taskManagementContext.SaveChangesAsync();
        }
    }
}
