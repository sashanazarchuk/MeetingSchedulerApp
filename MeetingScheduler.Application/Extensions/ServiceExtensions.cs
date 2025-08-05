using FluentValidation;
using MeetingScheduler.Application.Commands.CreateMeeting;
using MeetingScheduler.Application.Commands.CreateUser;
using MeetingScheduler.Application.DTOs.Meeting;
using MeetingScheduler.Application.Interfaces;
using MeetingScheduler.Application.Profile;
using MeetingScheduler.Application.Services;
using MeetingScheduler.Application.Validation;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Application.Extensions
{
    public static class ServiceExtensions
    {

        // Registers the application services with the dependency injection container.
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));
            services.AddSingleton<IMeetingSchedulerService, MeetingSchedulerService>();
            services.AddSingleton<IValidator<CreateMeetingDto>, CreateMeetingDtoValidator>();
            services.AddFluentValidationAutoValidation();

            return services;

        }
    }
}
