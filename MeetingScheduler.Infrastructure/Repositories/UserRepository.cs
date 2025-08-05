using MeetingScheduler.Application.Interfaces;
using MeetingScheduler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();

        public User CreateUser(User user)
        {
            user.Id = _users.Count + 1; 
            _users.Add(user);

            return user;
        }

        public List<User> GetAllUsers()
        {
            return _users.ToList();
        }

        public User? GetUserById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }
    }
}
