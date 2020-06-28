using DisTrack.Data;
using DisTrack.Data.Access.RepositoryInterfaces;
using DisTrack.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DisTrack.Domain.Services
{
    public class TripService : ITripService
    {
        private readonly IDisTrackRepository _repository;
        public TripService(IDisTrackRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddTrip(Trip trip)
        {
            try
            {
                _repository.Add(trip);
                await _repository.CommitAsync();

                return true;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
