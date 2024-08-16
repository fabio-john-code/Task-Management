using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementEntity.BusResults
{
    public record TaskItemListResult(Dictionary<Guid, string> TaskItems)
    {
    }
}
