using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementEntity.Model;
using static TaskManagementEntity.BusCommands.TaskItemGet;

namespace TaskManagementEntity.BusCommands
{
    public sealed record TaskItemGet(ActionEnum Action, Guid Id)
    {
        public enum ActionEnum
        {
            GetById,
            List,
        }
    }
}
