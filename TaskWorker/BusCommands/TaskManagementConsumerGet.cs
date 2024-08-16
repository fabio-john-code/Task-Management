using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementEntity.BusCommands;
using TaskManagementEntity.BusResults;
using TaskManagementEntity.Model;
using TaskWorker.TaskManagement.Interfaces;

namespace TaskWorker.Bus
{
    internal class TaskManagementConsumerGet : IConsumer<TaskItemGet>
    {
        private readonly ITaskManagementData _data;

        public TaskManagementConsumerGet(ITaskManagementData data)
        {
            _data = data;
        }

        public async Task Consume(ConsumeContext<TaskItemGet> context)
        {
            var taskItemCommand = context.Message;
            switch (taskItemCommand.Action)
            {
                case TaskItemGet.ActionEnum.GetById:
                    var taskItem = await _data.GetTaskById(taskItemCommand.Id);
                    await context.RespondAsync(new TaskItemGetByIdResult(taskItem));
                    break;
                case TaskItemGet.ActionEnum.List:
                    var listTasks = await _data.GetTasks();
                    await context.RespondAsync(new TaskItemListResult(listTasks));
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
