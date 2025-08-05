using FakeItEasy;
using MeetingScheduler.Application.Interfaces;
using MeetingScheduler.Application.Services;
using MeetingScheduler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Tests.UnitTests
{
    public class MeetingSchedulerServiceTests
    {

        [Fact]
        public void FindEarliestAvailableSlotTest()
        {
            // Arrange 
            // Create a fake repository using FakeItEasy
            var fakeRepository = A.Fake<IMeetingRepository>();
            var service = new MeetingSchedulerService(fakeRepository);

            // Define input parameters 
            var participantIds = new List<int> { 1, 2 };
            var earliestStart = new DateTime(2025, 8, 5, 9, 0, 0);
            var latestEnd = new DateTime(2025, 8, 5, 17, 0, 0);
            var duration = 60;

            // Set up the fake repository to return an empty list of meetings
            A.CallTo(() => fakeRepository.GetOverlappingMeetings(
                A<List<int>>._,
                A<DateTime>._,
                A<DateTime>._)).Returns(new List<Meeting>());

            // Act 
            // Call the method under test
            var result = service.FindEarliestAvailableSlot(participantIds, duration, earliestStart, latestEnd);

            // Assert
            // Verify that the result is not null and matches the expected earliest start time
            Assert.NotNull(result);
            Assert.Equal(earliestStart, result);
        }


        [Fact]
        public void FindEarliestAvailableSlot_WithOverlappingMeetings_ReturnsNextAvailableSlotTest()
        {
            // Arrange
            // Create a fake repository using FakeItEasy
            var fakeRepository = A.Fake<IMeetingRepository>();
            var service = new MeetingSchedulerService(fakeRepository);

            // Define input parameters
            var participantIds = new List<int> { 1, 2 };
            var earliestStart = new DateTime(2025, 8, 5, 9, 0, 0);
            var latestEnd = new DateTime(2025, 8, 5, 17, 0, 0);
            var duration = 60;

            // Create a list of overlapping meetings
            var meetings = new List<Meeting>
            {
                new Meeting { StartTime = new DateTime(2025, 8, 5, 9, 0, 0), EndTime = new DateTime(2025, 8, 5, 10, 0, 0), ParticipantIds = participantIds }
            };

            // Set up the fake repository to return a list of overlapping meetings
            A.CallTo(() => fakeRepository.GetOverlappingMeetings(
                A<List<int>>._,
                A<DateTime>._,
                A<DateTime>._)).Returns(meetings);


            // Act
            // Call the method under test
            var result = service.FindEarliestAvailableSlot(participantIds, duration, earliestStart, latestEnd);

            // Assert
            // Verify that the result is not null and matches the expected next available slot
            Assert.NotNull(result);
            Assert.Equal(new DateTime(2025, 8, 5, 10, 0, 0), result);
        }


        [Fact]
        public void FindEarliestAvailableSlot_WithBackToBackMeeting_ReturnsNextAvailableSlotTest()
        {
            // Arrange
            // Create a fake repository using FakeItEasy
            var fakeRepository = A.Fake<IMeetingRepository>();
            var service = new MeetingSchedulerService(fakeRepository);

            // Define input parameters
            var participantIds = new List<int> { 1, 2 };
            var earliestStart = new DateTime(2025, 8, 5, 9, 0, 0);
            var latestEnd = new DateTime(2025, 8, 5, 17, 0, 0);
            var duration = 60;


            // Create a list of meetings that overlap with the earliest start time
            var meetings = new List<Meeting>
            {
                new Meeting { StartTime = new DateTime(2025, 8, 5, 9, 0, 0), EndTime = new DateTime(2025, 8, 5, 10, 0, 0), ParticipantIds = participantIds },
                new Meeting { StartTime = new DateTime(2025, 8, 5, 10, 0, 0), EndTime = new DateTime(2025, 8, 5, 11, 0, 0), ParticipantIds = participantIds }
            };


            // Set up the fake repository to return these overlapping meetings
            A.CallTo(() => fakeRepository.GetOverlappingMeetings(
                A<List<int>>._,
                A<DateTime>._,
                A<DateTime>._)).Returns(meetings);

            // Act
            // Execute the method to find the earliest free slot
            var result = service.FindEarliestAvailableSlot(participantIds, duration, earliestStart, latestEnd);

            // Assert
            // Verify that the result is not null and matches the expected next available slot
            Assert.NotNull(result);
            Assert.Equal(new DateTime(2025, 8, 5, 11, 0, 0), result);
        }


        [Fact]
        public void FindEarliestAvailableSlot_NoAvailableSlot_ReturnsNull()
        {
            // Arrange
            // Create a fake repository using FakeItEasy
            var fakeRepository = A.Fake<IMeetingRepository>();
            var service = new MeetingSchedulerService(fakeRepository);

            // Define input parameters
            var participantIds = new List<int> { 1, 2 };
            var earliestStart = new DateTime(2025, 8, 5, 9, 0, 0);
            var latestEnd = new DateTime(2025, 8, 5, 12, 0, 0);
            var duration = 60;

            // Create a list of meetings that occupy the entire time window
            var meetings = new List<Meeting>
            {
                 new Meeting { StartTime = new DateTime(2025, 8, 5, 9, 0, 0), EndTime = new DateTime(2025, 8, 5, 10, 0, 0), ParticipantIds = participantIds },
                 new Meeting { StartTime = new DateTime(2025, 8, 5, 10, 0, 0), EndTime = new DateTime(2025, 8, 5, 11, 0, 0), ParticipantIds = participantIds },
                 new Meeting { StartTime = new DateTime(2025, 8, 5, 11, 0, 0), EndTime = new DateTime(2025, 8, 5, 12, 0, 0), ParticipantIds = participantIds }
            };


            // Set up the fake repository to return these overlapping meetings
            A.CallTo(() => fakeRepository.GetOverlappingMeetings(
                A<List<int>>._,
                A<DateTime>._,
                A<DateTime>._)).Returns(meetings);

            // Act
            // Execute the method to find the earliest free slot
            var result = service.FindEarliestAvailableSlot(participantIds, duration, earliestStart, latestEnd);

            // Assert
            // Verify that the result is null since there are no available slots
            Assert.Null(result);
        }


        [Fact]
        public void FindEarliestAvailableSlot_EarliestStartAfterBusinessHours_StartsAtEarliestStart()
        {
            // Arrange
            // Create a fake repository using FakeItEasy
            var fakeRepository = A.Fake<IMeetingRepository>();
            var service = new MeetingSchedulerService(fakeRepository);

            // Define input parameters
            var participantIds = new List<int> { 1 };
            var earliestStart = new DateTime(2025, 8, 5, 11, 0, 0);
            var latestEnd = new DateTime(2025, 8, 5, 17, 0, 0);
            var duration = 60;

            // Set up the fake repository to return an empty list of meetings
            A.CallTo(() => fakeRepository.GetOverlappingMeetings(
                A<List<int>>._,
                A<DateTime>._,
                A<DateTime>._)).Returns(new List<Meeting>());

            // Act
            // Execute the method to find the earliest free slot
            var result = service.FindEarliestAvailableSlot(participantIds, duration, earliestStart, latestEnd);

            // Assert
            // Verify that the result is not null and matches the expected earliest start time
            Assert.NotNull(result);
            Assert.Equal(earliestStart, result);
        }

    }
}