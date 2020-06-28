using DisTrack.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DisTrack.Domain.Interfaces
{
    public interface IUserService
    {
        Task<bool> AddUser(User user);
        Task<User> GetUserByEmail(string email);
    }
}
