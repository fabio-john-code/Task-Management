using MassTransit;
using TaskRestAPI.Controllers;
using TaskManagementEntity.Model;
using TaskManagementEntity.BusCommands;
using TaskRestAPI.Business.Interfaces;
using TaskManagementEntity.BusResults;

namespace TaskRestAPI.Business
{
    public class TaskManagementBusiness : ITaskManagementBusiness
    {
        private readonly ILogger<TaskManagementController> _logger;
        private readonly IBus _bus;
        private readonly IRequestClient<TaskItemGet> _clientRequestGet;

        public TaskManagementBusiness(IRequestClient<TaskItemGet> clientRequestGet,
            ILogger<TaskManagementController> logger,
            IBus bus)
        {
            _logger = logger;
            _bus = bus;
            _clientRequestGet = clientRequestGet;
        }

        public async Task<TaskItem> CreateTaskItem(string description)
        {
            try
            {
                var taskItem = new TaskItem
                { 
                    Id = Guid.NewGuid(),
                    Status = TaskItem.StatusTask.Created,
                    DateCreated = DateTimeOffset.Now,
                    Description = description,
                };

                await _bus.Publish(new TaskItemCreateOrUpdate(TaskItemCreateOrUpdate.ActionEnum.Create, taskItem));

                return taskItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Task Creation failed");
                throw;
            }
        }

        public async Task<TaskItem> GetTaskById(Guid id)
        {
            var response = await _clientRequestGet.GetResponse<TaskItemGetByIdResult>(new TaskItemGet(TaskItemGet.ActionEnum.GetById, id));
            var result = response.Message;
            return result.TaskItem;
        }

        public async Task<Dictionary<Guid, string>> GetTasks()
        {
            try
            {
                var response = await _clientRequestGet.GetResponse<TaskItemListResult>(new TaskItemGet(TaskItemGet.ActionEnum.List, Guid.Empty));
                var result = response.Message;
                return result.TaskItems;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TaskItem> UpdateTaskItemStatus(Guid id, TaskItem.StatusTask status)
        {
            var taskItem = this.GetTaskById(id).Result;

            taskItem.Status = status;
            taskItem.DateUpdated = DateTimeOffset.Now;

            await _bus.Publish(new TaskItemCreateOrUpdate(TaskItemCreateOrUpdate.ActionEnum.Update, taskItem));

            return taskItem;
        }
    }
}
