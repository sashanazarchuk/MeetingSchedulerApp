using AutoMapper;
using FluentValidation;
using MediatR;
using MeetingScheduler.Application.Commands.CreateMeeting;
using MeetingScheduler.Application.DTOs.Meeting;
using MeetingScheduler.Application.DTOs.MeetingDto;
using MeetingScheduler.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace MeetingScheduler.API.Controllers
{
    [Route("api/meetings")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MeetingController(IMediator mediator )
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpPost]
        public async Task<IActionResult> CreateMeeting([FromBody] CreateMeetingDto dto)
        {
            var createdMeeting = await _mediator.Send(new CreateMeetingCommand(dto));
            return Created("", createdMeeting);
        }
    }
}