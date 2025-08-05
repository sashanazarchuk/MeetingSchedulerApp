using MediatR;
using MeetingScheduler.Application.DTOs.MeetingDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Application.Queries.GetMeetingsByUser
{
    // Query to retrieve meetings by user ID, returning a collection of MeetingDto
    public record GetMeetingsByUsersQuery(int id): IRequest<IEnumerable<MeetingDto>>;

}
