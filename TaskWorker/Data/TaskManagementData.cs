using MassTransit;
using TaskManagementEntity.Model;
using TaskWorker.TaskManagement.Interfaces;

namespace TaskWorker.TaskManagement
{
    public class TaskManagementData : ITaskManagementData
    {
        private readonly ILogger<TaskManagementData> _logger;
        private readonly IBus _bus;

        private static List<TaskItem> taskItems = [];

        public TaskManagementData(ILogger<TaskManagementData> logger,
            IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        public async Task CreateTaskItem(TaskItem taskItem)
        {
            try
            {
                taskItems.Add(taskItem);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Task Creation failed");
                throw;
            }
        }

        public async Task<TaskItem> GetTaskById(Guid id)
        {
            await Task.Delay(500);
            return taskItems.Find(x => x.Id == id);
        }

        public async Task<Dictionary<Guid, string>> GetTasks()
        {
            var result = new Dictionary<Guid, string>();
            foreach (var task in taskItems)
            {
                result.Add(task.Id, task.Description);
            }
            await Task.Delay(500);

            return result;
        }

        public async Task UpdateTaskItem(TaskItem taskItemNew)
        {
            var taskItem = taskItems.Find(x => x.Id == taskItemNew.Id);
            taskItem.Description = taskItemNew.Description;
            taskItem.Status = taskItemNew.Status;
            await Task.Delay(500);
        }
    }
}
