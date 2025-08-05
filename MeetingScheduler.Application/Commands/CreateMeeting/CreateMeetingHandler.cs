using AutoMapper;
using MediatR;
using MeetingScheduler.Application.DTOs.MeetingDto;
using MeetingScheduler.Application.Exceptions;
using MeetingScheduler.Application.Interfaces;
using MeetingScheduler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Application.Commands.CreateMeeting
{
    public class CreateMeetingHandler : IRequestHandler<CreateMeetingCommand, MeetingDto>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMeetingSchedulerService _meetingSchedulerService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateMeetingHandler(IMeetingRepository meetingRepository, IMapper mapper, IUserRepository userRepository, IMeetingSchedulerService meetingSchedulerService)
        {
            _meetingRepository = meetingRepository ?? throw new ArgumentNullException(nameof(meetingRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userRepository = userRepository ?? throw new ArgumentException(nameof(userRepository));
            _meetingSchedulerService = meetingSchedulerService ?? throw new ArgumentException(nameof(meetingSchedulerService));
        }

        public Task<MeetingDto> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
        {
            // Validate that each participant exists
            foreach (var participantId in request.Dto.ParticipantIds)
            {
                var userExists = _userRepository.GetUserById(participantId);
                if (userExists == null)
                    throw new NotFoundException($"User with ID {participantId} not found.");
            }

            // Find the earliest available time slot for the meeting
            var slotStart = _meetingSchedulerService.FindEarliestAvailableSlot(
           request.Dto.ParticipantIds,
           request.Dto.DurationMinutes,
           request.Dto.EarliestStart,
           request.Dto.LatestEnd);

            // If no slot is found, throw an exception
            if (slotStart == null)
                throw new NotFoundException("No available time slot found for the requested participants.");
            

            // Create the meeting entity with calculated start and end times
            var meeting = new Meeting
            {

                ParticipantIds = request.Dto.ParticipantIds,
                StartTime = slotStart.Value,
                EndTime = slotStart.Value.AddMinutes(request.Dto.DurationMinutes)
            };

            // Persist the meeting
            _meetingRepository.CreateMeeting(meeting);

            // Map the created meeting to a DTO
            var dto = _mapper.Map<MeetingDto>(meeting);
            return Task.FromResult(dto);
        }
    }
}