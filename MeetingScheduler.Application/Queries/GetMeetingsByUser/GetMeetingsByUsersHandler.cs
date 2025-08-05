using AutoMapper;
using MediatR;
using MeetingScheduler.Application.DTOs.MeetingDto;
using MeetingScheduler.Application.Exceptions;
using MeetingScheduler.Application.Interfaces;
using MeetingScheduler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Application.Queries.GetMeetingsByUser
{
    public class GetMeetingsByUsersHandler : IRequestHandler<GetMeetingsByUsersQuery, IEnumerable<MeetingDto>>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMapper _mapper;

        public GetMeetingsByUsersHandler(IMeetingRepository meetingRepository, IMapper mapper)
        {
            _meetingRepository = meetingRepository ?? throw new ArgumentNullException(nameof(meetingRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<IEnumerable<MeetingDto>> Handle(GetMeetingsByUsersQuery request, CancellationToken cancellationToken)
        {
            // Retrieve meetings from repository by user ID
            var meetings = _meetingRepository.GetMeetingsByUser(request.id);

            // Throw exception if no meetings are found
            if (meetings == null || !meetings.Any())
                throw new NotFoundException($"No meetings found for user with ID {request.id}");

            // Map domain meetings to data transfer objects
            var meetingDtos = _mapper.Map<IEnumerable<MeetingDto>>(meetings);
            // Return the mapped meeting DTOs
            return Task.FromResult(meetingDtos);
        }
    }
}
