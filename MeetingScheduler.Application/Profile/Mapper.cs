using MeetingScheduler.Application.DTOs.MeetingDto;
using MeetingScheduler.Application.DTOs.User;
using MeetingScheduler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Application.Profile
{
    public class Mapper:AutoMapper.Profile  
    {
        // AutoMapper configuration for mapping domain entities and DTOs
        public Mapper()
        {
            CreateMap<Meeting, MeetingDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
