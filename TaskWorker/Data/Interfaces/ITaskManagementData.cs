using TaskManagementEntity.Model;

namespace TaskWorker.TaskManagement.Interfaces
{
    public interface ITaskManagementData
    {
        Task<TaskItem> GetTaskById(Guid id);
        Task<Dictionary<Guid, string>> GetTasks();
        Task CreateTaskItem(TaskItem taskItem);
        Task UpdateTaskItem(TaskItem taskItem);
    }
}
