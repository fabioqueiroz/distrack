using DisTrack.Data;
using DisTrack.Data.Access.RepositoryInterfaces;
using DisTrack.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DisTrack.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IDisTrackRepository _repository;
        public UserService(IDisTrackRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> AddUser(User user)
        {
            try
            {
                _repository.Add(user);
                await _repository.CommitAsync();

                return true;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                return await _repository.GetSingleAsync<User>(x => x.Email.Equals(email));
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
