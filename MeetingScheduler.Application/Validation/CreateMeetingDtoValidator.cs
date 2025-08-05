using FluentValidation;
using MeetingScheduler.Application.Commands.CreateMeeting;
using MeetingScheduler.Application.DTOs.Meeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Application.Validation
{
    public class CreateMeetingDtoValidator : AbstractValidator<CreateMeetingDto>
    {
        // Validates the CreateMeetingDto to ensure it meets the required criteria
        public CreateMeetingDtoValidator()
        {
            RuleFor(x => x.DurationMinutes)
                .GreaterThan(0)
                .WithMessage("Meeting duration must be greater than 0.");

            RuleFor(x => x.ParticipantIds)
                .NotNull()
                .NotEmpty()
                .WithMessage("At least one participant is required.");

            RuleFor(x => x.EarliestStart)
                .LessThan(x => x.LatestEnd)
                .WithMessage("Earliest start must be before latest end.");
        }
    }
}
