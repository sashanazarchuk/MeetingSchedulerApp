using MeetingScheduler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Application.Interfaces
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        List<User> GetAllUsers();
        User? GetUserById(int id);
    }
}
