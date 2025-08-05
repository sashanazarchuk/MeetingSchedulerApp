using MediatR;
using MeetingScheduler.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Application.Queries.GetAllUsers
{

    // Query to retrieve all users, returning a collection of UserDto
    public record GetAllUsersQuery:IRequest<IEnumerable<UserDto>>;

}
