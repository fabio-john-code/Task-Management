using TaskManagementEntity.Model;

namespace TaskWorker.TaskManagement.Interfaces
{
    public interface ITaskManagementData
    {
        Task<TaskItem> GetTaskById(int id);
        Task<List<TaskItem>> GetTasks();
        Task CreateTaskItem(TaskItem taskItem);
        Task UpdateTaskItem(TaskItem taskItem);
    }
}
