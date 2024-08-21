using TaskManagementEntity.Model;

namespace TaskRestAPI.Business.Interfaces
{
    public interface ITaskManagementBusiness
    {
        Task<TaskItem> GetTaskById(int id);
        Task<List<TaskItem>> GetTasks();
        Task<TaskItem> CreateTaskItem(string description);
        Task<TaskItem> UpdateTaskItemStatus(int id, TaskItem.StatusTask status);
    }
}
