using MeetingScheduler.Application.Interfaces;
using MeetingScheduler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Application.Services
{
    public class MeetingSchedulerService : IMeetingSchedulerService
    {
        private readonly IMeetingRepository _meetingRepository;

        public MeetingSchedulerService(IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        public DateTime? FindEarliestAvailableSlot(List<int> participantIds, int durationMinutes, DateTime earliestStart, DateTime latestEnd)
        {

            // Start from the earliest start date
            DateTime currentDay = earliestStart.Date;
            DateTime endDay = latestEnd.Date;

            // Iterate over each day from earliestStart to latestEnd
            while (currentDay <= endDay)
            {
                // Get the business hours window for the current day
                var (windowStart, windowEnd) = GetBussinesHoursWindow(currentDay, earliestStart, latestEnd);

                // If the business hours window is invalid, skip to the next day
                if (windowStart >= windowEnd)
                {
                    currentDay = currentDay.AddDays(1);
                    continue;
                }

                // Get all meetings that overlap with the current business hours window
                var relevantMeetings = _meetingRepository.GetOverlappingMeetings(participantIds, windowStart, windowEnd);
                var slot = FindAvailableTimeSlot(relevantMeetings, durationMinutes, windowStart, windowEnd);

                // If a valid time slot is found, return it
                if (slot != null)
                    return slot;

                // Move to the next day
                currentDay = currentDay.AddDays(1);
            }

            // If no available slot is found, return null
            return null;
        }


        private (DateTime windowStart, DateTime windowEnd) GetBussinesHoursWindow(DateTime day, DateTime earliestStart, DateTime latestEnd)
        {
            // Define business hours (9 AM to 5 PM)
            TimeSpan businessStart = new TimeSpan(9, 0, 0);
            TimeSpan businessEnd = new TimeSpan(17, 0, 0);

            // Calculate the start and end of the business hours for the given day
            DateTime dayStart = day.Add(businessStart);
            DateTime dayEnd = day.Add(businessEnd);

            // Ensure the business hours window is within the earliest and latest bounds
            DateTime windowStart = earliestStart > dayStart ? earliestStart : dayStart;
            DateTime windowEnd = latestEnd < dayEnd ? latestEnd : dayEnd;

            // If the window is invalid, return the same start and end
            return (windowStart, windowEnd);
        }

       

        private DateTime? FindAvailableTimeSlot(List<Meeting> meetings, int durationMinutes, DateTime windowStart, DateTime windowEnd)
        {
            // Initialize the start and end of the potential meeting slot
            DateTime slotStart = windowStart;
            DateTime slotEnd = slotStart.AddMinutes(durationMinutes);

            // If the initial slot is already beyond the window, return null
            while (slotEnd <= windowEnd)
            {
                // Check if the current slot overlaps with any existing meetings
                bool overlaps = meetings.Any(m => slotStart < m.EndTime && slotEnd > m.StartTime);

                // If no overlap, return the slot start time
                if (!overlaps)
                {
                    return slotStart;
                }

                // If it overlaps, find the next available slot by moving to the end of the next meeting
                var nextMeeting = meetings.FirstOrDefault(m => m.StartTime >= slotStart);
                if (nextMeeting != null && nextMeeting.StartTime < slotEnd)
                {
                    slotStart = nextMeeting.EndTime;
                }
                else
                {
                    slotStart = slotStart.AddMinutes(1);
                }

                // Update the end of the slot based on the new start time
                slotEnd = slotStart.AddMinutes(durationMinutes);
            }
            // If no valid slot is found within the window, return null
            return null;
        }
    }
}
