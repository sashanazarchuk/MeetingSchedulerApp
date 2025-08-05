using MeetingScheduler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Application.Interfaces
{
    public interface IMeetingRepository
    {
        Meeting CreateMeeting(Meeting meeting);
        IEnumerable<Meeting> GetMeetingsByUser(int userId);
        List<Meeting> GetOverlappingMeetings(List<int> participantIds, DateTime windowStart, DateTime windowEnd);
    }
}
