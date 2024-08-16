using MassTransit;
using TaskManagementEntity.Model;
using TaskManagementEntity.BusCommands;
using TaskWorker.TaskManagement.Interfaces;

namespace TaskWorker.BusCommands
{
    public class TaskManagementConsumerCreateOrUpdate : IConsumer<TaskItemCreateOrUpdate>
    {
        private readonly ILogger<TaskManagementConsumerCreateOrUpdate> _logger;
        private readonly ITaskManagementData _data;

        public TaskManagementConsumerCreateOrUpdate(ITaskManagementData data,
            ILogger<TaskManagementConsumerCreateOrUpdate> logger) 
        {
            _logger = logger;
            _data = data;
        }

        public Task Consume(ConsumeContext<TaskItemCreateOrUpdate> context)
        {
            var taskItemCommand = context.Message;
            _logger.LogInformation($"New Task Item: {taskItemCommand.TaskItem.Id} - {taskItemCommand.TaskItem.Description}");

            switch (taskItemCommand.Action)
            {
                case TaskItemCreateOrUpdate.ActionEnum.Create:
                    _data.CreateTaskItem(taskItemCommand.TaskItem);
                    break;
                case TaskItemCreateOrUpdate.ActionEnum.Update:
                    _data.UpdateTaskItem(taskItemCommand.TaskItem);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return Task.CompletedTask;
        }
    }
}
