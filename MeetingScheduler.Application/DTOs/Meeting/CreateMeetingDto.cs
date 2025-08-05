using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Application.DTOs.Meeting
{
    public class CreateMeetingDto
    {
        public List<int> ParticipantIds { get; set; } = new List<int>();
        public int DurationMinutes { get; set; }
        public DateTime EarliestStart { get; set; }
        public DateTime LatestEnd { get; set; }

    }
}