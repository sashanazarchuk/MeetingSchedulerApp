using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Application.Interfaces
{
    public interface IMeetingSchedulerService
    {
        DateTime? FindEarliestAvailableSlot(List<int> participantIds, int durationMinutes, DateTime earliestStart, DateTime latestEnd);
    }
}
