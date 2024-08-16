using MassTransit;
using TaskManagementEntity.Model;
using TaskManagementEntity.BusCommands;

namespace TaskWorker.Bus
{
    public class TaskManagementConsumerCreate : IConsumer<TaskItemCreate>
    {
        private readonly ILogger<TaskManagementConsumerCreate> _logger;

        public TaskManagementConsumerCreate(ILogger<TaskManagementConsumerCreate> logger) 
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<TaskItemCreate> context)
        {
            var taskItem = context.Message.TaskItem;
            _logger.LogInformation($"New Task Item: {taskItem.Id} - {taskItem.Description}");

            return Task.CompletedTask;
        }
    }
}
