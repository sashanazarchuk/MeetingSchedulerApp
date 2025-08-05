using MediatR;
using MeetingScheduler.Application.Commands.CreateUser;
using MeetingScheduler.Application.DTOs.User;
using MeetingScheduler.Application.Queries.GetAllUsers;
using MeetingScheduler.Application.Queries.GetMeetingsByUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingScheduler.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            var createdUser = await _mediator.Send(new CreateUserCommand(dto));
            return Created("", createdUser);
        }


        [HttpGet("{userId}/meetings")]
        public async Task<IActionResult> GetMeetingsByUser(int userId)
        {
            var users = await _mediator.Send(new GetMeetingsByUsersQuery(userId));
            return Ok(users);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }
    }
}