using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementEntity.Model;

namespace TaskManagementEntity.BusResults
{
    public record TaskItemListResult(List<TaskItem> TaskItems)
    {
    }
}
