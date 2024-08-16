using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementEntity.Model;
using static TaskManagementEntity.BusCommands.TaskItemCreateOrUpdate;

namespace TaskManagementEntity.BusCommands
{
    public sealed record TaskItemCreateOrUpdate(ActionEnum Action, TaskItem TaskItem)
    {
        public enum ActionEnum
        {
            Create,
            Update,
        }
    }
}
