using TaskManagementEntity.Model;

namespace TaskRestAPI.Business.Interfaces
{
    public interface ITaskManagementBusiness
    {
        Task<TaskItem> GetTaskById(Guid id);
        Task<Dictionary<Guid, string>> GetTasks();
        Task<TaskItem> CreateTaskItem(string description);
        Task<TaskItem> UpdateTaskItemStatus(Guid id, TaskItem.StatusTask status);
    }
}
