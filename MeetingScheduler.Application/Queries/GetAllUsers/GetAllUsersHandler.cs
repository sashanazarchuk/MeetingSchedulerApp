using AutoMapper;
using MediatR;
using MeetingScheduler.Application.DTOs.User;
using MeetingScheduler.Application.Exceptions;
using MeetingScheduler.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Application.Queries.GetAllUsers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsersHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            // Retrieve all users from the repository
            var users = _userRepository.GetAllUsers();

            // Map the list of User entities to UserDto
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

            // Return the mapped user DTOs
            return Task.FromResult(userDtos);
        }
    }
}
