using MeetingScheduler.Application.Interfaces;
using MeetingScheduler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Infrastructure.Repositories
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly List<Meeting> _meetings = new List<Meeting>();

        public Meeting CreateMeeting(Meeting meeting)
        {
            meeting.Id = _meetings.Count > 0 ? _meetings.Max(m => m.Id) + 1 : 1;
            _meetings.Add(meeting);
            return meeting;
        }

         
        public IEnumerable<Meeting> GetMeetingsByUser(int userId)
        {
            return _meetings.Where(m => m.ParticipantIds.Contains(userId));
        }


        public List<Meeting> GetOverlappingMeetings(List<int> participantIds, DateTime windowStart, DateTime windowEnd)
        {
            return _meetings
                .Where(m => m.ParticipantIds.Any(id => participantIds.Contains(id)) &&
                            m.EndTime > windowStart && m.StartTime < windowEnd)
                .OrderBy(m => m.StartTime)
                .ToList();
        }
    }
}