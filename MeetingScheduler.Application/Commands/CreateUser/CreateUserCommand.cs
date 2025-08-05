using MediatR;
using MeetingScheduler.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Application.Commands.CreateUser
{
    // Command to create a user with the provided DTO
    public record CreateUserCommand(CreateUserDto Dto): IRequest<UserDto>;

}