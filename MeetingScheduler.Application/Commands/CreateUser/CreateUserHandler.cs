using AutoMapper;
using MediatR;
using MeetingScheduler.Application.DTOs.User;
using MeetingScheduler.Application.Interfaces;
using MeetingScheduler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Application.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //Create a new user 
            var user = new User
            {
                Name = request.Dto.Name
            };

            // Persist the user 
            var createdUser = _userRepository.CreateUser(user);

            // Map the created user to a DTO
            var result = _mapper.Map<UserDto>(createdUser);
            return Task.FromResult(result);
        }
    }
}