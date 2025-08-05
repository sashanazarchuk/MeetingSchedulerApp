using MediatR;
using MeetingScheduler.Application.DTOs.Meeting;
using MeetingScheduler.Application.DTOs.MeetingDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Application.Commands.CreateMeeting
{

    // Command to create a meeting with the provided DTO
    public record CreateMeetingCommand(CreateMeetingDto Dto) : IRequest<MeetingDto>;  

}
